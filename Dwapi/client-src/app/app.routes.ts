import {Routes, RouterModule} from '@angular/router';
import {ModuleWithProviders} from '@angular/core';
import {DashboardComponent} from '../dashboard/dashboard.component';
import {RegistryConfigComponent} from '../settings/registry-config/registry-config.component';
import {EmrConfigComponent} from '../settings/emrs/emr-config/emr-config.component';
import { NdwhDocketComponent } from '../dockets/ndwh-docket/ndwh-docket.component';
import { PsmartDocketComponent } from '../dockets/psmart-docket/psmart-docket.component';
import { CbsDocketComponent } from '../dockets/cbs-docket/cbs-docket.component';

export const routes: Routes = [
    {path: '', component: DashboardComponent},
    {path: 'registryconfig', component: RegistryConfigComponent},
    {path: 'emrconfig', component: EmrConfigComponent},
    {path: 'datawarehouse', component: NdwhDocketComponent},
    {path: 'psmart', component: PsmartDocketComponent},
    {path: 'cbs', component: CbsDocketComponent},
];

export const AppRoutes: ModuleWithProviders = RouterModule.forRoot(routes);
