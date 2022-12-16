import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SkillCellComponent } from './skill-cell.component';

describe('SkillCellComponent', () => {
  let component: SkillCellComponent;
  let fixture: ComponentFixture<SkillCellComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SkillCellComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SkillCellComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
