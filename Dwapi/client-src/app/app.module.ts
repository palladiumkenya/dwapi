import {NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HttpModule} from '@angular/http';
import {BrowserModule} from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {LocationStrategy, HashLocationStrategy, CommonModule} from '@angular/common';
import {AppRoutes} from './app.routes';
import 'rxjs/add/operator/toPromise';

import {AccordionModule} from 'primeng/primeng';
import {AutoCompleteModule} from 'primeng/primeng';
import {BreadcrumbModule} from 'primeng/primeng';
import {ButtonModule} from 'primeng/primeng';
import {CalendarModule} from 'primeng/primeng';
import {CarouselModule} from 'primeng/primeng';
import {ColorPickerModule} from 'primeng/primeng';
import {ChartModule} from 'primeng/primeng';
import {CheckboxModule} from 'primeng/primeng';
import {ChipsModule} from 'primeng/primeng';
import {CodeHighlighterModule} from 'primeng/primeng';
import {ConfirmDialogModule} from 'primeng/primeng';
import {SharedModule} from 'primeng/primeng';
import {ContextMenuModule} from 'primeng/primeng';
import {DataGridModule} from 'primeng/primeng';
import {DataListModule} from 'primeng/primeng';
import {DataScrollerModule} from 'primeng/primeng';
import {DataTableModule} from 'primeng/primeng';
import {DialogModule} from 'primeng/primeng';
import {DragDropModule} from 'primeng/primeng';
import {DropdownModule} from 'primeng/primeng';
import {EditorModule} from 'primeng/primeng';
import {FieldsetModule} from 'primeng/primeng';
import {FileUploadModule} from 'primeng/primeng';
import {GalleriaModule} from 'primeng/primeng';
import {GMapModule} from 'primeng/primeng';
import {GrowlModule} from 'primeng/primeng';
import {InputMaskModule} from 'primeng/primeng';
import {InputSwitchModule} from 'primeng/primeng';
import {InputTextModule} from 'primeng/primeng';
import {InputTextareaModule} from 'primeng/primeng';
import {LightboxModule} from 'primeng/primeng';
import {ListboxModule} from 'primeng/primeng';
import {MegaMenuModule} from 'primeng/primeng';
import {MenuModule} from 'primeng/primeng';
import {MenubarModule} from 'primeng/primeng';
import {MessagesModule} from 'primeng/primeng';
import {MultiSelectModule} from 'primeng/primeng';
import {OrderListModule} from 'primeng/primeng';
import {OrganizationChartModule} from 'primeng/primeng';
import {OverlayPanelModule} from 'primeng/primeng';
import {PaginatorModule} from 'primeng/primeng';
import {PanelModule} from 'primeng/primeng';
import {PanelMenuModule} from 'primeng/primeng';
import {PasswordModule} from 'primeng/primeng';
import {PickListModule} from 'primeng/primeng';
import {ProgressBarModule} from 'primeng/primeng';
import {RadioButtonModule} from 'primeng/primeng';
import {RatingModule} from 'primeng/primeng';
import {ScheduleModule} from 'primeng/primeng';
import {SelectButtonModule} from 'primeng/primeng';
import {SlideMenuModule} from 'primeng/primeng';
import {SliderModule} from 'primeng/primeng';
import {SpinnerModule} from 'primeng/primeng';
import {SplitButtonModule} from 'primeng/primeng';
import {StepsModule} from 'primeng/primeng';
import {TabMenuModule} from 'primeng/primeng';
import {TabViewModule} from 'primeng/primeng';
import {TerminalModule} from 'primeng/primeng';
import {TieredMenuModule} from 'primeng/primeng';
import {ToggleButtonModule} from 'primeng/primeng';
import {ToolbarModule} from 'primeng/primeng';
import {TooltipModule} from 'primeng/primeng';
import {TreeModule} from 'primeng/primeng';
import {TreeTableModule} from 'primeng/primeng';
import {TableModule} from 'primeng/table';

import {AppComponent} from './app.component';
import {RegistryConfigComponent} from '../settings/registry-manager/registry-config/registry-config.component';
import {EmrConfigComponent} from '../settings/emrs/emr-config/emr-config.component';
import { TopbarComponent } from './topbar/topbar.component';
import { InlineProfileComponent } from './inline-profile/inline-profile.component';
import { FooterComponent } from './footer/footer.component';
import { RightpanelComponent } from './rightpanel/rightpanel.component';
import { AppMenuComponent } from './app-menu/app-menu.component';
import { AppSubmenuComponent } from './app-submenu/app-submenu.component';
import { AppBreadcrumbComponent } from './app-breadcrumb/app-breadcrumb.component';
import {BreadcrumbService} from './breadcrumb.service';
import { DashboardComponent } from '../dashboard/dashboard.component';
import {MessageService} from 'primeng/components/common/messageservice';
import {ConfirmationService} from 'primeng/api';
import {RegistryConfigService} from '../settings/services/registry-config.service';
import {HttpClientModule} from '@angular/common/http';
import {MessageModule} from 'primeng/message';
import {EmrConfigService} from '../settings/services/emr-config.service';
import { DatabaseProtocolConfigComponent } from '../settings/emrs/database-protocol-config/database-protocol-config.component';
import { RestProtocolConfigComponent } from '../settings/emrs/rest-protocol-config/rest-protocol-config.component';
import {ProtocolConfigService} from '../settings/services/protocol-config.service';
import { ExtractConfigComponent } from '../settings/emrs/extract-config/extract-config.component';
import {ExtractConfigService} from '../settings/services/extract-config.service';
import { PsmartConsoleComponent } from '../dockets/psmart-docket/psmart-console/psmart-console.component';
import {PsmartExtractService} from '../dockets/services/psmart-extract.service';
import { PsmartMiddlewareConsoleComponent } from '../dockets/psmart-docket/psmart-middleware-console/psmart-middleware-console.component';
import {PsmartSenderService} from '../dockets/services/psmart-sender.service';
import { NdwhConsoleComponent } from '../dockets/ndwh-docket/ndwh-console/ndwh-console.component';
import { NdwhExtractService } from '../dockets/services/ndwh-extract.service';
import { NdwhSenderService } from '../dockets/services/ndwh-sender.service';
import { NdwhDocketComponent } from '../dockets/ndwh-docket/ndwh-docket.component';
import { PsmartDocketComponent } from '../dockets/psmart-docket/psmart-docket.component';
import { CbsDocketComponent } from '../dockets/cbs-docket/cbs-docket.component';
import { NdwhExtractDetailsComponent } from '../dockets/ndwh-docket/ndwh-extract-details/ndwh-extract-details.component';
import { NdwhPatientsExtractService } from '../dockets/services/ndwh-patients-extract.service';
import { DbProtocolComponent } from '../settings/db-protocol/db-protocol.component';
// tslint:disable-next-line:max-line-length
import { ValidRecordDetailsComponent } from '../dockets/ndwh-docket/ndwh-extract-details/valid-record-details/valid-record-details.component';
// tslint:disable-next-line:max-line-length
import { InvalidRecordDetailsComponent } from '../dockets/ndwh-docket/ndwh-extract-details/invalid-record-details/invalid-record-details.component';
import {EmrDocketComponent} from '../settings/emr-docket/emr-docket.component';
import {EmrSettingsComponent} from '../settings/emr-settings/emr-settings.component';
import {CbsService} from '../dockets/services/cbs.service';
import { NdwhPatientArtService } from '../dockets/services/ndwh-patient-art.service';
import { NdwhPatientBaselineService } from '../dockets/services/ndwh-patient-baseline.service';
import { NdwhPatientLaboratoryService } from '../dockets/services/ndwh-patient-laboratory.service';
import { NdwhPatientPharmacyService } from '../dockets/services/ndwh-patient-pharmacy.service';
import { NdwhPatientStatusService } from '../dockets/services/ndwh-patient-status.service';
import { NdwhPatientVisitService } from '../dockets/services/ndwh-patient-visit.service';
import { RegistryManagerComponent } from '../settings/registry-manager/registry-manager.component';
import { SetupComponent } from '../settings/setup/setup.component';
import {SetupService} from '../settings/services/setup.service';

@NgModule({
    imports: [
        CommonModule,
        BrowserModule,
        FormsModule,
        ReactiveFormsModule,
        AppRoutes,
        HttpModule,
        HttpClientModule,
        BrowserAnimationsModule,
        AccordionModule,
        AutoCompleteModule,
        BreadcrumbModule,
        ButtonModule,
        CalendarModule,
        CarouselModule,
        ColorPickerModule,
        ChartModule,
        CheckboxModule,
        ChipsModule,
        CodeHighlighterModule,
        ConfirmDialogModule,
        SharedModule,
        ContextMenuModule,
        DataGridModule,
        DataListModule,
        DataScrollerModule,
        DataTableModule,
        DialogModule,
        DragDropModule,
        DropdownModule,
        EditorModule,
        FieldsetModule,
        FileUploadModule,
        GalleriaModule,
        GMapModule,
        GrowlModule,
        InputMaskModule,
        InputSwitchModule,
        InputTextModule,
        InputTextareaModule,
        LightboxModule,
        ListboxModule,
        MegaMenuModule,
        MenuModule,
        MenubarModule,
        MessageModule,
        MessagesModule,
        MultiSelectModule,
        OrderListModule,
        OrganizationChartModule,
        OverlayPanelModule,
        PaginatorModule,
        PanelModule,
        PanelMenuModule,
        PasswordModule,
        PickListModule,
        ProgressBarModule,
        RadioButtonModule,
        RatingModule,
        ScheduleModule,
        SelectButtonModule,
        SlideMenuModule,
        SliderModule,
        SpinnerModule,
        SplitButtonModule,
        StepsModule,
        TabMenuModule,
        TabViewModule,
        TerminalModule,
        TieredMenuModule,
        ToggleButtonModule,
        ToolbarModule,
        TooltipModule,
        TreeModule,
        TreeTableModule,
        TableModule
    ],
    declarations: [
        AppComponent,
        RegistryConfigComponent,
        EmrConfigComponent,
        TopbarComponent,
        InlineProfileComponent,
        FooterComponent,
        RightpanelComponent,
        AppMenuComponent,
        AppSubmenuComponent,
        AppBreadcrumbComponent,
        DashboardComponent,
        DatabaseProtocolConfigComponent,
        RestProtocolConfigComponent,
        ExtractConfigComponent,
        PsmartConsoleComponent,
        PsmartMiddlewareConsoleComponent,
        NdwhConsoleComponent,
        NdwhDocketComponent,
        PsmartDocketComponent,
        CbsDocketComponent,
        NdwhExtractDetailsComponent,
        DbProtocolComponent,
        ValidRecordDetailsComponent,
        InvalidRecordDetailsComponent,
        EmrSettingsComponent,
        EmrDocketComponent,
        RegistryManagerComponent,
        SetupComponent
    ],
    providers: [
        {provide: LocationStrategy, useClass: HashLocationStrategy}, BreadcrumbService,
        MessageService, ConfirmationService, RegistryConfigService, EmrConfigService, ProtocolConfigService,
        ExtractConfigService, PsmartExtractService , PsmartSenderService, NdwhExtractService, NdwhSenderService, NdwhPatientsExtractService,
        CbsService, NdwhPatientArtService, NdwhPatientBaselineService, NdwhPatientLaboratoryService, NdwhPatientPharmacyService,
        NdwhPatientStatusService, NdwhPatientVisitService,
        SetupService
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }

