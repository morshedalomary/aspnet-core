import { ChangeDetectionStrategy, Component } from '@angular/core';
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
import { Router } from '@angular/router';

@Component({
  selector: 'app-content',
  changeDetection: ChangeDetectionStrategy.Default,
  standalone: true,
  imports: [
    ...ChildTabDependencies,
    NgbCollapseModule,
    NgbDatepickerModule,
    NgbDropdownModule,

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
  templateUrl: './content.component.html',
  styles: `::ng-deep.datatable-row-detail { background: transparent !important; }`,
})
export class ContentComponent extends AbstractContentComponent {


  constructor(
    private contentService : ContentService,
    public readonly router: Router,

  ) 
  {
    super();
  }

  CreateContent() {

    this.router.navigate(['/contents/create-update' , ''])
  }

  
  UpdateContent(record: ContentDto) {

    this.router.navigate(['/contents/create-update', record.id])
  }

}
