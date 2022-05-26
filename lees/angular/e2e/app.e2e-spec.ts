import { MyprojectTemplatePage } from './app.po';

describe('Myproject App', function() {
  let page: MyprojectTemplatePage;

  beforeEach(() => {
    page = new MyprojectTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
