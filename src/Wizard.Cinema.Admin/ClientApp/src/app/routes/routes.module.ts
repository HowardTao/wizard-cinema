import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';
import { RouteRoutingModule } from './routes-routing.module';
// dashboard pages
import { DashboardV1Component } from './dashboard/v1/v1.component';
import { DashboardAnalysisComponent } from './dashboard/analysis/analysis.component';
import { DashboardMonitorComponent } from './dashboard/monitor/monitor.component';
import { DashboardWorkplaceComponent } from './dashboard/workplace/workplace.component';
// passport pages
import { UserLoginComponent } from './passport/login/login.component';
import { UserRegisterComponent } from './passport/register/register.component';
import { UserRegisterResultComponent } from './passport/register-result/register-result.component';
// single pages
import { UserLockComponent } from './passport/lock/lock.component';
import { Exception403Component } from './exception/403.component';
import { Exception404Component } from './exception/404.component';
import { Exception500Component } from './exception/500.component';

//cinema pages
import { CinemaShows1Component } from './cinema/shows1/shows1.component';

import { HeadWizardsComponent } from './wizards/head-wizards/head-wizards.component';
import { DivisionsComponent } from './wizards/divisions/divisions.component';
import { ActivityListComponent } from './activity/list/activity-list.component';
import { ActivityDetailComponent } from './activity/detail/activity-detail.component';
import { ApplicantListComponent } from './activity/applicants/applicant-list.component';
import { SessionListComponent } from './cinema/sessions/session-list/session-list.component';
import { SessionEditComponent } from './cinema/sessions/session-edit/session-edit.component';;
import { SessionTaskComponent } from './cinema/sessions/session-tasks/session-tasks.component';

const COMPONENTS = [
  DashboardV1Component,
  DashboardAnalysisComponent,
  DashboardMonitorComponent,
  DashboardWorkplaceComponent,
  // passport pages
  UserLoginComponent,
  UserRegisterComponent,
  UserRegisterResultComponent,
  // single pages
  UserLockComponent,
  Exception403Component,
  Exception404Component,
  Exception500Component,
  //cinema pages
  CinemaShows1Component,
  HeadWizardsComponent,
  DivisionsComponent,
  ActivityListComponent,
  ActivityDetailComponent,
  ApplicantListComponent,
  SessionListComponent,
  SessionEditComponent,
  SessionTaskComponent
];
const COMPONENTS_NOROUNT = [];

@NgModule({
  imports: [SharedModule, RouteRoutingModule],
  declarations: [...COMPONENTS, ...COMPONENTS_NOROUNT],
  entryComponents: COMPONENTS_NOROUNT
})
export class RoutesModule { }
