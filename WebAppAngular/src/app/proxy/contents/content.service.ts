import type {
  ContentCreateDto,
  ContentDto,
  ContentExcelDownloadDto,
  ContentUpdateDto,
  GetContentsInput,
} from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { DownloadTokenResultDto } from '../shared/models';

@Injectable({
  providedIn: 'root',
})
export class ContentService {
  apiName = 'Default';

  insertOrUpdateCMSContent = (id : string , name : string , value : string , config?: Partial<Rest.Config>) =>
    this.restService.request<any, ContentDto>(
      {
        method: 'POST',
        url: '/api/app/plugin/contents/insert-update-content',
        params: {
          id: id,
      
          name: name,
          value: value,
        },
     
      },
      { apiName: this.apiName, ...config }
    );

    getCMSContent = (id: string, config?: Partial<Rest.Config>) =>
      this.restService.request<any, ContentDto>(
        {
          method: 'GET',
          url: `/api/app/plugin/contents/get-cms-content/${id}`,
        },
        { apiName: this.apiName, ...config }
      );
      getAll = ( config?: Partial<Rest.Config>) =>
        this.restService.request<any, ContentDto[]>(
          {
            method: 'GET',
            url: `/api/app/plugin/contents/get-all`,
          },
          { apiName: this.apiName, ...config }
        );

    create = (input: ContentCreateDto, config?: Partial<Rest.Config>) =>
      this.restService.request<any, ContentDto>(
        {
          method: 'POST',
          url: '/api/app/plugin/contents',
          body: input,
        },
        { apiName: this.apiName, ...config }
      );

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: `/api/app/plugin/contents/${id}`,
      },
      { apiName: this.apiName, ...config }
    );

  deleteAll = (input: GetContentsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: '/api/app/plugin/contents/all',
        params: {
          filterText: input.filterText,
          sorting: input.sorting,
          skipCount: input.skipCount,
          maxResultCount: input.maxResultCount,
          name: input.name,
          value: input.value,
        },
      },
      { apiName: this.apiName, ...config }
    );

  deleteByIds = (contentIds: string[], config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: '/api/app/plugin/contents',
        params: { contentIds },
      },
      { apiName: this.apiName, ...config }
    );

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ContentDto>(
      {
        method: 'GET',
        url: `/api/app/plugin/contents/${id}`,
      },
      { apiName: this.apiName, ...config }
    );

  getDownloadToken = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, DownloadTokenResultDto>(
      {
        method: 'GET',
        url: '/api/app/plugin/contents/download-token',
      },
      { apiName: this.apiName, ...config }
    );

  getList = (input: GetContentsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<ContentDto>>(
      {
        method: 'GET',
        url: '/api/app/plugin/contents',
        params: {
          filterText: input.filterText,
          sorting: input.sorting,
          skipCount: input.skipCount,
          maxResultCount: input.maxResultCount,
          name: input.name,
          value: input.value,
        },
      },
      { apiName: this.apiName, ...config }
    );

  getListAsExcelFile = (input: ContentExcelDownloadDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>(
      {
        method: 'GET',
        responseType: 'blob',
        url: '/api/app/plugin/contents/as-excel-file',
        params: {
          downloadToken: input.downloadToken,
          filterText: input.filterText,
          name: input.name,
          value: input.value,
        },
      },
      { apiName: this.apiName, ...config }
    );

  update = (id: string, input: ContentUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ContentDto>(
      {
        method: 'PUT',
        url: `/api/app/plugin/contents/${id}`,
        body: input,
      },
      { apiName: this.apiName, ...config }
    );

  constructor(private restService: RestService) {}
}
