import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

const oAuthConfig = {
  issuer: 'https://localhost:44383/',
  redirectUri: baseUrl,
  clientId: 'MasterProject_App',
  responseType: 'code',
  scope: 'offline_access MasterProject',
  requireHttps: true,
};

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'MasterProject',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://localhost:44383',
      rootNamespace: 'MasterProject',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },
} as Environment;
