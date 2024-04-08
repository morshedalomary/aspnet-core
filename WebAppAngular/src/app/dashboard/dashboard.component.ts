import { Component } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  template: `
    <app-host-dashboard *abpPermission="'MasterProject.Dashboard.Host'"></app-host-dashboard>
    <app-tenant-dashboard *abpPermission="'MasterProject.Dashboard.Tenant'"></app-tenant-dashboard>
  `,
})
export class DashboardComponent {}
