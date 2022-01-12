import { async, ComponentFixture, TestBed } from "@angular/core/testing";
import { RouterTestingModule } from "@angular/router/testing";
import { BreadcrumbService } from "../breadcrumb.service";
import { AppBreadcrumbComponent } from "./app-breadcrumb.component";

describe("AppBreadcrumbComponent", () => {
    let component: AppBreadcrumbComponent;
    let fixture: ComponentFixture<AppBreadcrumbComponent>;
    //let service: BreadcrumbService;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [RouterTestingModule],
            declarations: [AppBreadcrumbComponent],
            providers:[BreadcrumbService]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(AppBreadcrumbComponent);
        component = fixture.componentInstance;

        fixture.detectChanges();
    });

    it("should create", () => {
        expect(component).toBeTruthy();
    });
});
