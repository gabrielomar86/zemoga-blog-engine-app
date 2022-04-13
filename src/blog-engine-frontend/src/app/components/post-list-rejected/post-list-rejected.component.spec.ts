import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PostListRejectedComponent } from './post-list-rejected.component';

describe('PostListComponent', () => {
  let component: PostListRejectedComponent;
  let fixture: ComponentFixture<PostListRejectedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PostListRejectedComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PostListRejectedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
