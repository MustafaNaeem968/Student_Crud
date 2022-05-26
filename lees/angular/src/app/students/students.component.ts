import {
  Component,
  OnInit,
  Injector,
  EventEmitter,
  Output,
} from "@angular/core";
import { BsModalService, BsModalRef } from "ngx-bootstrap/modal";
import {
  StudentsServiceProxy,
  StudentDto,
  StudentDtoPagedResultDto,
} from "@shared/service-proxies/service-proxies";
import { AppComponentBase } from "@shared/app-component-base";
import { CreateStudentComponent } from "./create-student/create-student.component";
import {
  PagedListingComponentBase,
  PagedRequestDto,
} from "@shared/paged-listing-component-base";
import { finalize } from "rxjs/operators";
import { result } from "lodash-es";

class PagedStudentRequestDto extends PagedRequestDto {
  keyword: string;
}

@Component({
  selector: "app-students",
  templateUrl: "./students.component.html",
  styleUrls: [],
})
export class StudentsComponent extends PagedListingComponentBase<StudentDto> {
  students: StudentDto[] = [];
  searchText: StudentDto;
  keyword = "";
  isActive: boolean | null;
  advancedFiltersVisible = false;

  constructor(
    injector: Injector,
    private _studentService: StudentsServiceProxy,
    private _modalService: BsModalService
  ) {
    super(injector);
  }

  CreateStudent(): void {
    this.showCreateOrEditUserDialog();
  }

  editStudent(student: StudentDto): void {
    this.showCreateOrEditUserDialog(student.id);
  }

  clearFilters(): void {
    this.keyword = "";
    this.isActive = undefined;
    this.getDataPage(1);
  }

  list(
    request: PagedStudentRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    request.keyword = this.keyword;
    debugger;
    this._studentService
      .getAll(request.maxResultCount, request.skipCount)
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result: StudentDtoPagedResultDto) => {
        this.students = result.items;
        this.showPaging(result, pageNumber);
      });
  }
  search(name: any) {
    debugger;
    this._studentService.getAllStudentList(name).subscribe((result) => {
      this.students = result;
    });
    if (name == undefined || name == "") {
      this.refresh();
    }
  }
  delete(student: StudentDto): void {
    abp.message.confirm(
      this.l("Are You Realy Wanted to delete ", student.name, "?"),
      undefined,
      (result: boolean) => {
        if (result) {
          this._studentService.delete(student.id).subscribe(() => {
            abp.notify.success(this.l("SuccessfullyDeleted"));
            this.refresh();
          });
        }
      }
    );
  }

  showCreateOrEditUserDialog(id?: number): void {
    let createOrEditUserDialog: BsModalRef;
    if (!id) {
      createOrEditUserDialog = this._modalService.show(CreateStudentComponent, {
        class: "modal-lg",
      });
    } else {
      createOrEditUserDialog = this._modalService.show(CreateStudentComponent, {
        class: "modal-lg",
        initialState: {
          id: id,
        },
      });
    }
    createOrEditUserDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }
}
