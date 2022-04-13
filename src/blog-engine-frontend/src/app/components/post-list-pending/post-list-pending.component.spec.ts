import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PostListPendingComponent } from './post-list-pending.component';

describe('PostListComponent', () => {
  let component: PostListPendingComponent;
  let fixture: ComponentFixture<PostListPendingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PostListPendingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PostListPendingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
