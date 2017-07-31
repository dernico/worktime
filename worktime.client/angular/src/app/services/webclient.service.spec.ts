/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { WebclientService } from './webclient.service';

describe('WebclientService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [WebclientService]
    });
  });

  it('should ...', inject([WebclientService], (service: WebclientService) => {
    expect(service).toBeTruthy();
  }));
});
