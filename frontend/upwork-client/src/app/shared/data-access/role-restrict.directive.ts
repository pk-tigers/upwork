import {
  Directive,
  Input,
  OnInit,
  TemplateRef,
  ViewContainerRef,
} from '@angular/core';
import { UserService } from './user.service';

@Directive({
  selector: '[appRoleRestrict]',
})
export class RoleRestrictDirective implements OnInit {
  @Input() roleRestrict = '';

  constructor(
    private userService: UserService,
    private templateRef: TemplateRef<any>,
    private viewContainerRef: ViewContainerRef
  ) {}

  ngOnInit(): void {
    this.userService.user$.subscribe(user => {
      if (user?.role?.includes(this.roleRestrict)) {
        this.viewContainerRef.createEmbeddedView(this.templateRef);
      } else {
        this.viewContainerRef.clear();
      }
    });
  }
}
