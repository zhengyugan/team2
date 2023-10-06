import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent, NgbdModalContent } from './app.component';
import { RouterModule } from '@angular/router';
import { NgChartsModule } from 'ng2-charts';
import { ProductModule } from './user/product/product.module';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { SectionSalesComponent } from './report/sections/section-sales/section-sales.component';
import { SectionOrdersComponent } from './report/sections/section-orders/section-orders.component';
import { SalesDataService } from './report/services/sales-data.service';
import { BarChartComponent } from './report/charts/bar-chart/bar-chart.component';
import { PieChartComponent } from './report/charts/pie-chart/pie-chart.component';
import { LineChartComponent } from './report/charts/line-chart/line-chart.component';
import { appRoutes } from '../routes';
import { FontAwesomeModule, FaIconLibrary } from '@fortawesome/angular-fontawesome';
import { faCartShopping, faUser,faPlus,faMinus } from '@fortawesome/free-solid-svg-icons';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from './shared/shared.module';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    SectionSalesComponent,
    SectionOrdersComponent,
    BarChartComponent,
    LineChartComponent,
    PieChartComponent,
    NgbdModalContent,
    LoginComponent,
    SignupComponent
    ],
    exports: [
        SharedModule
    ],
    providers: [
        SalesDataService,
        FontAwesomeModule
    ],
    bootstrap: [AppComponent, NgbdModalContent],
    imports: [
        BrowserModule,
        HttpClientModule,
        RouterModule.forRoot(appRoutes),
        ProductModule,
        NgChartsModule,
        FontAwesomeModule,
        ReactiveFormsModule,
        SharedModule
    ]
})
export class AppModule {
  constructor(library: FaIconLibrary) {
    library.addIcons(
      faCartShopping,
      faUser,
      faPlus,
      faMinus
      );
  }
 }
