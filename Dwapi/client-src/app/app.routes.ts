import {Routes, RouterModule} from '@angular/router';
import {ModuleWithProviders} from '@angular/core';
import {DashboardComponent} from '../dashboard/dashboard.component';
import {RegistryConfigComponent} from '../settings/registry-manager/registry-config/registry-config.component';
import {EmrConfigComponent} from '../settings/emrs/emr-config/emr-config.component';
import { NdwhDocketComponent } from '../dockets/ndwh-docket/ndwh-docket.component';
import { PsmartDocketComponent } from '../dockets/psmart-docket/psmart-docket.component';
import { CbsDocketComponent } from '../dockets/cbs-docket/cbs-docket.component';
import {EmrSettingsComponent} from '../settings/emr-settings/emr-settings.component';
import {RegistryManagerComponent} from '../settings/registry-manager/registry-manager.component';

export const routes: Routes = [
    {path: 'dashboard', component: DashboardComponent},
    {path: 'registry/:docketId', component: RegistryManagerComponent },
    {path: 'emrconfig', component: EmrSettingsComponent},
    {path: 'datawarehouse', component: NdwhDocketComponent},
    {path: 'psmart', component: PsmartDocketComponent},
    {path: 'cbs', component: CbsDocketComponent},
    {path: '', redirectTo: '/dashboard', pathMatch: 'full'},
    {path: '**', component: DashboardComponent }
];

export const AppRoutes: ModuleWithProviders = RouterModule.forRoot(routes);
