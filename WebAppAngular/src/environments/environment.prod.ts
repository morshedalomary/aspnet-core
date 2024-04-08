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
  production: true,
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
 /* Enables environment override by dynamic-env.json file for docker/kubernetes deployment.
  *  Uncomment below if you are using docker or kubernetes deployment.
  *  See https://docs.abp.io/en/commercial/latest/startup-templates/application/deployment-docker-compose?UI=NG&DB=EF&Tiered=No#angular  
  */
  // remoteEnv: {
  //   url: '/getEnvConfig',
  //   mergeStrategy: 'deepmerge'
  // }
} as Environment;
