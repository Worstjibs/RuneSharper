import { TestBed } from '@angular/core/testing';

import { CharacterViewResolver } from './character-view.resolver';

describe('CharacterViewResolver', () => {
  let resolver: CharacterViewResolver;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    resolver = TestBed.inject(CharacterViewResolver);
  });

  it('should be created', () => {
    expect(resolver).toBeTruthy();
  });
});
