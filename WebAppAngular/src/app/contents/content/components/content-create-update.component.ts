import { ChangeDetectionStrategy, Component, ViewChild } from '@angular/core';
import {
  NgbDateAdapter,
  NgbCollapseModule,
  NgbDatepickerModule,
  NgbDropdownModule,
} from '@ng-bootstrap/ng-bootstrap';
import { NgxValidateCoreModule } from '@ngx-validate/core';
import { ListService, CoreModule } from '@abp/ng.core';
import { ThemeSharedModule, DateAdapter } from '@abp/ng.theme.shared';
import { PageModule } from '@abp/ng.components/page';
import { CommercialUiModule } from '@volo/abp.commercial.ng.ui';

import { ContentViewService } from '../services/content.service';
import { ContentDetailViewService } from '../services/content-detail.service';
import { ContentDetailModalComponent } from './content-detail.component';
import {
  AbstractContentComponent,
  ChildTabDependencies,
  ChildComponentDependencies,
} from './content.abstract.component';
import { ContentDto, ContentService } from '@proxy/contents';
import { ActivatedRoute } from '@angular/router';

import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';


import { CKEditorModule, ChangeEvent } from '@ckeditor/ckeditor5-angular';
import { Location } from '@angular/common'


@Component({
  selector: 'app-content-create-update',
  changeDetection: ChangeDetectionStrategy.Default,
  standalone: true,
  imports: [
    ...ChildTabDependencies,
    NgbCollapseModule,
    NgbDatepickerModule,
    NgbDropdownModule,
    CKEditorModule,
    NgxValidateCoreModule,

    PageModule,
    CoreModule,
    ThemeSharedModule,
    CommercialUiModule,
    ContentDetailModalComponent,
    ...ChildComponentDependencies,
  ],
  providers: [
    ListService,
    ContentViewService,
    ContentDetailViewService,
    { provide: NgbDateAdapter, useClass: DateAdapter },
  ],
  templateUrl: './content-create-update.component.html',
  styles: `::ng-deep.datatable-row-detail { background: transparent !important; }`,
})



export class ContentCreateUpdateComponent extends AbstractContentComponent {

  contentId: string;
  contentDto : ContentDto 
  nameContent : string
  public Editor = ClassicEditor;

  actionName: string ;
  data: any = `<p>Hello, world!</p>`;

  
  constructor(
    private activatedRoute : ActivatedRoute,
    private location: Location,
    private contentService : ContentService,

  ) {
    super();
  
  }



  ngOnInit(): void {



    this.actionName ="Create";

    this.contentDto =
    {
      name : "",
      value :"",
      id :""
    }
    this.contentId = this.activatedRoute.snapshot.params["contentId"];
    if(this.contentId)
      {

        this.actionName = "Update";
        this.contentService.getCMSContent(this.contentId).subscribe(res => {
          this.contentDto = res
          this.nameContent = this.contentDto.name
          this.data = this.contentDto.value
        })  
      }
      

    }

  

    onSubmit()
    {
          this.contentDto.name = this.nameContent;
       
          this.contentService.insertOrUpdateCMSContent(this.contentDto.id , this.contentDto.name , this.contentDto.value).subscribe(res => {

              this.location.back();


          })
        
       
  }

    back(){

      this.location.back();

    }
}
