import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './home/feature/login/login.component';
import { OrganizationControlComponent } from './organization-control/organization-control.component';

const routes: Routes = [{
   path: 'login', component: LoginComponent },
   { path: 'organization-control', component: OrganizationControlComponent }
  ];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
