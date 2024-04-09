import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface ContentCreateDto {
  name?: string;
  value?: string;
}

export interface ContentCreateUpdateDto {
  id?: string
  name?: string;
  value?: string;
}


export interface ContentDto extends FullAuditedEntityDto<string> {
  name?: string;
  value?: string;
  concurrencyStamp?: string;
}

export interface ContentExcelDownloadDto {
  downloadToken?: string;
  filterText?: string;
  name?: string;
  value?: string;
}

export interface ContentUpdateDto {
  name?: string;
  value?: string;
  concurrencyStamp?: string;
}

export interface GetContentsInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  name?: string;
  value?: string;
}
