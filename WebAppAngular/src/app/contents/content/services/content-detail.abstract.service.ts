import { inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ListService, TrackByService } from '@abp/ng.core';
import { finalize, tap } from 'rxjs/operators';

import type { ContentDto } from '../../../proxy/contents/models';
import { ContentService } from '../../../proxy/contents/content.service';

export abstract class AbstractContentDetailViewService {
  protected readonly fb = inject(FormBuilder);
  protected readonly track = inject(TrackByService);
  public readonly proxyService = inject(ContentService);
  public readonly list = inject(ListService);

  isBusy = false;
  isVisible = false;
  selected = {} as any;
  form: FormGroup | undefined;

  buildForm() {
    const { name, value } = this.selected || {};

    this.form = this.fb.group({
      name: [name ?? null, []],
      value: [value ?? null, []],
    });
  }

  showForm() {
    this.buildForm();
    this.isVisible = true;
  }

  create() {
    this.selected = undefined;
    this.showForm();
  }

  update(record: ContentDto) {
    this.selected = record;
    this.showForm();
  }

  hideForm() {
    this.isVisible = false;
    this.form.reset();
  }

  submitForm() {
    if (this.form.invalid) return;

    this.isBusy = true;

    const request = this.createRequest().pipe(
      finalize(() => (this.isBusy = false)),
      tap(() => this.hideForm())
    );

    request.subscribe(this.list.get);
  }

  private createRequest() {
    const formValues = {
      ...this.form.value,
    };

    if (this.selected) {
      return this.proxyService.update(this.selected.id, {
        ...formValues,
        concurrencyStamp: this.selected.concurrencyStamp,
      });
    }

    return this.proxyService.create(formValues);
  }

  changeVisible($event: boolean) {
    this.isVisible = $event;
  }
}
