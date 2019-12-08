import { TestBed } from '@angular/core/testing';

import { ResultHandlingService } from './result-handling.service';

describe('ResultHandlingService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ResultHandlingService = TestBed.get(ResultHandlingService);
    expect(service).toBeTruthy();
  });
});
