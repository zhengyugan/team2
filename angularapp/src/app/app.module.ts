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
import { PaginationComponent } from './report/pagination/pagination.component';
import { appRoutes } from '../routes';
import { FontAwesomeModule, FaIconLibrary } from '@fortawesome/angular-fontawesome';
import { faCartShopping, faUser,faPlus,faMinus } from '@fortawesome/free-solid-svg-icons';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { ReactiveFormsModule } from '@angular/forms';

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
    PaginationComponent,
    NgbdModalContent,
    LoginComponent,
    SignupComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot(appRoutes),
    ProductModule,
    NgChartsModule,
    FontAwesomeModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [
    SalesDataService,
    FontAwesomeModule
  ],
  bootstrap: [AppComponent, NgbdModalContent],
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
