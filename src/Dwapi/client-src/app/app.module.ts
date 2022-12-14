import {NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HttpModule} from '@angular/http';
import {BrowserModule} from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {LocationStrategy, HashLocationStrategy, CommonModule} from '@angular/common';
import {AppRoutes} from './app.routes';
import 'rxjs/add/operator/toPromise';

import {AccordionModule, Card, CardModule} from 'primeng/primeng';
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
import {TopbarComponent} from './topbar/topbar.component';
import {InlineProfileComponent} from './inline-profile/inline-profile.component';
import {FooterComponent} from './footer/footer.component';
import {RightpanelComponent} from './rightpanel/rightpanel.component';
import {AppMenuComponent} from './app-menu/app-menu.component';
import {AppSubmenuComponent} from './app-submenu/app-submenu.component';
import {AppBreadcrumbComponent} from './app-breadcrumb/app-breadcrumb.component';
import {BreadcrumbService} from './breadcrumb.service';
import {DashboardComponent} from '../dashboard/dashboard.component';
import {MessageService} from 'primeng/components/common/messageservice';
import {ConfirmationService} from 'primeng/api';
import {RegistryConfigService} from '../settings/services/registry-config.service';
import {HttpClientModule} from '@angular/common/http';
import {MessageModule} from 'primeng/message';
import {EmrConfigService} from '../settings/services/emr-config.service';
import {DatabaseProtocolConfigComponent} from '../settings/emrs/database-protocol-config/database-protocol-config.component';
import {RestProtocolConfigComponent} from '../settings/emrs/rest-protocol-config/rest-protocol-config.component';
import {ProtocolConfigService} from '../settings/services/protocol-config.service';
import {ExtractConfigComponent} from '../settings/emrs/extract-config/extract-config.component';
import {ExtractConfigService} from '../settings/services/extract-config.service';
import {PsmartConsoleComponent} from '../dockets/psmart-docket/psmart-console/psmart-console.component';
import {PsmartExtractService} from '../dockets/services/psmart-extract.service';
import {PsmartMiddlewareConsoleComponent} from '../dockets/psmart-docket/psmart-middleware-console/psmart-middleware-console.component';
import {PsmartSenderService} from '../dockets/services/psmart-sender.service';
import {NdwhConsoleComponent} from '../dockets/ndwh-docket/ndwh-console/ndwh-console.component';
import {NdwhExtractService} from '../dockets/services/ndwh-extract.service';
import {NdwhSenderService} from '../dockets/services/ndwh-sender.service';
import {NdwhDocketComponent} from '../dockets/ndwh-docket/ndwh-docket.component';
import {PsmartDocketComponent} from '../dockets/psmart-docket/psmart-docket.component';
import {CbsDocketComponent} from '../dockets/cbs-docket/cbs-docket.component';
import {NdwhExtractDetailsComponent} from '../dockets/ndwh-docket/ndwh-extract-details/ndwh-extract-details.component';
import {NdwhPatientsExtractService} from '../dockets/services/ndwh-patients-extract.service';
import {DbProtocolComponent} from '../settings/db-protocol/db-protocol.component';
// tslint:disable-next-line:max-line-length
import {ValidRecordDetailsComponent} from
        '../dockets/ndwh-docket/ndwh-extract-details/valid-record-details/valid-record-details.component';
// tslint:disable-next-line:max-line-length
import {InvalidRecordDetailsComponent} from '../dockets/ndwh-docket/ndwh-extract-details/invalid-record-details/invalid-record-details.component';
import {EmrDocketComponent} from '../settings/emr-docket/emr-docket.component';
import {EmrSettingsComponent} from '../settings/emr-settings/emr-settings.component';
import {CbsService} from '../dockets/services/cbs.service';
import {NdwhPatientArtService} from '../dockets/services/ndwh-patient-art.service';
import {NdwhPatientBaselineService} from '../dockets/services/ndwh-patient-baseline.service';
import {NdwhPatientLaboratoryService} from '../dockets/services/ndwh-patient-laboratory.service';
import {NdwhPatientPharmacyService} from '../dockets/services/ndwh-patient-pharmacy.service';
import {NdwhPatientStatusService} from '../dockets/services/ndwh-patient-status.service';
import {NdwhPatientVisitService} from '../dockets/services/ndwh-patient-visit.service';
import {RegistryManagerComponent} from '../settings/registry-manager/registry-manager.component';
import {SetupComponent} from '../settings/setup/setup.component';
import {SetupService} from '../settings/services/setup.service';
import {MpiSearchComponent} from '../dockets/cbs-docket/mpi-search/mpi-search.component';
import {MpiSearchService} from '../dockets/services/mpi-search.service';
import {NdwhPatientAdverseEventService} from '../dockets/services/ndwh-patient-adverse-event.service';
import {AppDetailsService} from './services/app-details.service';
import {RestProtocolComponent} from '../settings/rest-protocol/rest-protocol.component';
import {RestResourceComponent} from '../settings/rest-resource/rest-resource.component';
import {HtsDocketComponent} from '../dockets/hts-docket/hts-docket.component';
import {HtsService} from '../dockets/services/hts.service';
import {HtsConsoleComponent} from '../dockets/hts-docket/hts-console/hts-console.component';
import {HtsExtractDetailsComponent} from '../dockets/hts-docket/hts-extract-details/hts-extract-details.component';
import {HtsInvalidComponent} from '../dockets/hts-docket/hts-extract-details/hts-invalid/hts-invalid.component';
import {HtsValidComponent} from '../dockets/hts-docket/hts-extract-details/hts-valid/hts-valid.component';
import {HtsSenderService} from '../dockets/services/hts-sender.service';
import {HtsClientService} from '../dockets/services/hts-client.service';
import {HtsClientPartnerService} from '../dockets/services/hts-client-partner.service';
import {HtsClientLinkageService} from '../dockets/services/hts-client-linkage.service';
import {HtsEligibilityScreeningService} from '../dockets/services/hts-eligibility-screening.service';

import {HtsClientsService} from '../dockets/services/hts-clients.service';
import {HtsClientTestsService} from '../dockets/services/hts-client-tests.service';
import {HtsClientsLinkageService} from '../dockets/services/hts-clients-linkage.service';
import {HtsTestKitsService} from '../dockets/services/hts-test-kits.service';
import {HtsClientTracingService} from '../dockets/services/hts-client-tracing.service';
import {HtsPartnerTracingService} from '../dockets/services/hts-partner-tracing.service';
import {HtsPartnerNotificationServicesService} from '../dockets/services/hts-partner-notification-services.service';
import {MetricsComponent} from '../dashboard/metrics/metrics.component';
import {MetricsService} from '../dashboard/services/metrics.service';

import {MgsDocketComponent} from '../dockets/mgs-docket/mgs-docket.component';
import {MgsService} from '../dockets/services/mgs.service';
import {MgsConsoleComponent} from '../dockets/mgs-docket/mgs-console/mgs-console.component';
import {MgsExtractDetailsComponent} from '../dockets/mgs-docket/mgs-extract-details/mgs-extract-details.component';
import {MgsInvalidComponent} from '../dockets/mgs-docket/mgs-extract-details/mgs-invalid/mgs-invalid.component';
import {MgsValidComponent} from '../dockets/mgs-docket/mgs-extract-details/mgs-valid/mgs-valid.component';
import {MgsSenderService} from '../dockets/services/mgs-sender.service';
import {MetricMigrationService} from '../dockets/services/metric-migration.service';
import {NdwhSummaryService} from '../dockets/services/ndwh-summary.service';
import {MnchClientService} from '../dockets/services/mnch-client.service';
import {MnchSenderService} from '../dockets/services/mnch-sender.service';
import {MnchService} from '../dockets/services/mnch.service';
import {MnchClientLinkageService} from '../dockets/services/mnch-client-linkage.service';
import {MnchDocketComponent} from '../dockets/mnch-docket/mnch-docket.component';
import {MnchConsoleComponent} from '../dockets/mnch-docket/mnch-console/mnch-console.component';
import {MnchExtractDetailsComponent} from '../dockets/mnch-docket/mnch-extract-details/mnch-extract-details.component';
import {MnchInvalidComponent} from '../dockets/mnch-docket/mnch-extract-details/mnch-invalid/mnch-invalid.component';
import {MnchValidComponent} from '../dockets/mnch-docket/mnch-extract-details/mnch-valid/mnch-valid.component';
import {MnchSummaryService} from "../dockets/services/mnch-summary.service";
import {PrepDocketComponent} from "../dockets/prep-docket/prep-docket.component";
import {PrepConsoleComponent} from "../dockets/prep-docket/prep-console/prep-console.component";
import {PrepValidComponent} from "../dockets/prep-docket/prep-extract-details/prep-valid/prep-valid.component";
import {PrepInvalidComponent} from "../dockets/prep-docket/prep-extract-details/prep-invalid/prep-invalid.component";
import {PrepExtractDetailsComponent} from "../dockets/prep-docket/prep-extract-details/prep-extract-details.component";
import {PrepService} from "../dockets/services/prep.service";
import {PrepClientLinkageService} from "../dockets/services/prep-client-linkage.service";
import {PrepSummaryService} from "../dockets/services/prep-summary.service";
import {PrepClientService} from "../dockets/services/prep-client.service";
import {PrepSenderService} from "../dockets/services/prep-sender.service";
import {CrsService} from "../dockets/services/crs.service";
import { CrsDocketComponent } from "../dockets/crs-docket/crs-docket.component";
import { exportComponent } from "../exports/file-exports.component";
import { UploadComponent } from "../upload/file-upload.component";
import { DownloadComponent } from '../download/download.component';
import { UploadService } from '../dockets/services/upload-service';
import { DndDirective } from "../upload/upload.directive";


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
        TableModule,
        CardModule
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
        SetupComponent,
        MpiSearchComponent,
        RestProtocolComponent,
        RestResourceComponent,
        HtsDocketComponent,
        HtsConsoleComponent,
        HtsExtractDetailsComponent,
        HtsInvalidComponent,
        HtsValidComponent,
        MetricsComponent,
        MgsDocketComponent,
        MgsConsoleComponent,
        MgsExtractDetailsComponent,
        MgsInvalidComponent,
        MgsValidComponent,
        MnchDocketComponent,
        MnchConsoleComponent,
        MnchExtractDetailsComponent,
        MnchInvalidComponent,
        MnchValidComponent,
        PrepDocketComponent,
        PrepConsoleComponent,
        PrepExtractDetailsComponent,
        PrepInvalidComponent,
        PrepValidComponent,
        DownloadComponent,
        exportComponent,
        UploadComponent,
        DndDirective,
        CrsDocketComponent
],
    providers: [
        {provide: LocationStrategy, useClass: HashLocationStrategy}, BreadcrumbService,
        MessageService, ConfirmationService, RegistryConfigService, EmrConfigService, ProtocolConfigService,
        ExtractConfigService, PsmartExtractService, PsmartSenderService, NdwhExtractService, NdwhSenderService, NdwhPatientsExtractService,
        CbsService, NdwhPatientArtService, NdwhPatientBaselineService, NdwhPatientLaboratoryService, NdwhPatientPharmacyService,
        NdwhPatientStatusService, NdwhPatientVisitService,
        SetupService, MpiSearchService, NdwhPatientAdverseEventService, AppDetailsService, HtsService, HtsSenderService,
        HtsClientsService, HtsClientTestsService, HtsClientsLinkageService, HtsTestKitsService, HtsClientTracingService,
        HtsPartnerTracingService, HtsPartnerNotificationServicesService, HtsClientService, HtsClientPartnerService, HtsClientLinkageService,
        MetricsService, MgsService, MgsSenderService, MetricMigrationService, NdwhSummaryService,
        MnchService, MnchSenderService, MnchClientService, MnchClientLinkageService, MnchSummaryService,
        PrepService, PrepSenderService, PrepClientService, PrepClientLinkageService, PrepSummaryService,
        CrsService, HtsEligibilityScreeningService

    ],
    bootstrap: [AppComponent]
})
export class AppModule {
}

