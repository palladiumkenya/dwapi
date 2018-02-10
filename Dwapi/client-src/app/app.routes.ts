import {Routes, RouterModule} from '@angular/router';
import {ModuleWithProviders} from '@angular/core';
import {DashboardComponent} from '../dashboard/dashboard.component';
import {RegistryConfigComponent} from '../settings/registry-config/registry-config.component';
import {EmrConfigComponent} from '../settings/emrs/emr-config/emr-config.component';

export const routes: Routes = [
    {path: '', component: DashboardComponent},
    {path: 'registryconfig', component: RegistryConfigComponent},
    {path: 'emrconfig', component: EmrConfigComponent},
];

export const AppRoutes: ModuleWithProviders = RouterModule.forRoot(routes);
