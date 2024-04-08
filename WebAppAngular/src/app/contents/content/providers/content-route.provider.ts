import { eLayoutType, RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const CONTENTS_CONTENT_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routes: RoutesService) {
  return () => {
    routes.add([
      {
        path: '/contents',
        iconClass: 'fas fa-file-alt',
        name: '::Menu:Contents',
        layout: eLayoutType.application
      },
    ]);
  };
}
