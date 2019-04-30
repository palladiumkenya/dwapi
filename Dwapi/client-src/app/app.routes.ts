import {Routes, RouterModule} from '@angular/router';
import {ModuleWithProviders} from '@angular/core';
import {DashboardComponent} from '../dashboard/dashboard.component';
import { NdwhDocketComponent } from '../dockets/ndwh-docket/ndwh-docket.component';
import { PsmartDocketComponent } from '../dockets/psmart-docket/psmart-docket.component';
import { CbsDocketComponent } from '../dockets/cbs-docket/cbs-docket.component';
import {EmrSettingsComponent} from '../settings/emr-settings/emr-settings.component';
import {RegistryManagerComponent} from '../settings/registry-manager/registry-manager.component';
import { MpiSearchComponent } from '../dockets/cbs-docket/mpi-search/mpi-search.component';
import {HtsDocketComponent} from '../dockets/hts-docket/hts-docket.component';

export const routes: Routes = [
    {path: 'dashboard', component: DashboardComponent},
    {path: 'mpisearch', component: MpiSearchComponent },
    {path: 'registry/:docketId', component: RegistryManagerComponent },
    {path: 'emrconfig', component: EmrSettingsComponent},
    {path: 'datawarehouse', component: NdwhDocketComponent},
    {path: 'psmart', component: PsmartDocketComponent},
    {path: 'cbs', component: CbsDocketComponent},
    {path: 'hts', component: HtsDocketComponent},
    {path: '', redirectTo: '/dashboard', pathMatch: 'full'},
    {path: '**', component: DashboardComponent }
];

export const AppRoutes: ModuleWithProviders = RouterModule.forRoot(routes);
