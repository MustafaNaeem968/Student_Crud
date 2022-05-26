import {
  Component,
  OnInit,
  EventEmitter,
  Injector,
  Output,
} from "@angular/core";
import { AppComponentBase } from "@shared/app-component-base";
import {
  StudentDto,
  StudentDtoPagedResultDto,
  StudentsServiceProxy,
} from "@shared/service-proxies/service-proxies";
import { BsModalRef } from "ngx-bootstrap/modal";

@Component({
  selector: "app-create-student",
  templateUrl: "./create-student.component.html",
  styleUrls: [],
})
export class CreateStudentComponent extends AppComponentBase implements OnInit {
  saving = false;
  student = new StudentDto();
  checkedRolesMap: { [key: string]: boolean } = {};
  id: number;

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    public _studentService: StudentsServiceProxy,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    if (this.id > 0) {
      this._studentService.get(this.id).subscribe((result) => {
        this.student = result;
        console.log(result);
      });
    }
  }

  save(): void {
    this.saving = true;
    if (this.student.id > 0) {
      this._studentService.update(this.student).subscribe(
        () => {
          abp.notify.info("Update Successfully");
          this.bsModalRef.hide();
          this.onSave.emit();
        },
        () => {
          this.saving = false;
        }
      );
    } else {
      this._studentService.create(this.student).subscribe(
        () => {
          abp.notify.info("Saved Successfully");
          this.bsModalRef.hide();
          this.onSave.emit();
        },
        () => {
          this.saving = false;
        }
      );
    }
  }
}
