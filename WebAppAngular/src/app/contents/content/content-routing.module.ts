import { authGuard, permissionGuard } from '@abp/ng.core';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContentComponent } from './components/content.component';
import { ContentCreateUpdateComponent } from './components/content-create-update.component';

export const routes: Routes = [
  {
    path: '',
    component: ContentComponent,
  },
  
  {
    path :'create-update/:contentId',
    component : ContentCreateUpdateComponent,
    
},
{
  path :'create-update',
  component : ContentCreateUpdateComponent,
  
}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ContentRoutingModule {}
