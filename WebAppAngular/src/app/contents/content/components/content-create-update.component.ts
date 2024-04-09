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
import { ContentCreateUpdateDto, ContentDto, ContentService } from '@proxy/contents';
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
  contentCreateUpdateDto : ContentCreateUpdateDto
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


onReady(eventData) {
    eventData.plugins.get('FileRepository').createUploadAdapter = function (loader) {
      console.log(btoa(loader.file));
      return new UploadAdapter(loader);
    };
  }
  ngOnInit(): void {

    ClassicEditor
    .create( this.Editor, {
        cloudServices: {
            tokenUrl: 'https://example.com/cs-token-endpoint',
            uploadUrl: 'https://your-organization-id.cke-cs.com/easyimage/upload/'
        }
    } )
    .then( )
    .catch( );

    this.actionName ="Create";

    this.contentCreateUpdateDto =
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
          this.contentCreateUpdateDto = res
          this.nameContent = this.contentCreateUpdateDto.name
          this.data = this.contentCreateUpdateDto.value
        })  
      }
      

    }

  

    onSubmit()
    {
          this.contentCreateUpdateDto.name = this.nameContent;
       
          this.contentService.insertOrUpdateCMSContent(this.contentCreateUpdateDto).subscribe(res => {

              this.location.back();


          })
        
       
  }

    back(){

      this.location.back();

    }
}


export class UploadAdapter {
  private loader;
  constructor(loader: any) {
    this.loader = loader;
  }

  upload() {
    return this.loader.file
          .then( file => new Promise( ( resolve, reject ) => {
                var myReader= new FileReader();
                myReader.onloadend = (e) => {
                   resolve({ default: myReader.result });
                }

                myReader.readAsDataURL(file);
          } ) );
 };


}