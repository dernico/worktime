import { Worktime.ClientPage } from './app.po';

describe('worktime.client App', () => {
  let page: Worktime.ClientPage;

  beforeEach(() => {
    page = new Worktime.ClientPage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!');
  });
});
