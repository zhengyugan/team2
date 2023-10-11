import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatNativeDateModule } from '@angular/material/core';
import { FaIconLibrary, FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faCartShopping, faUser, faPlus, faMinus } from '@fortawesome/free-solid-svg-icons';
import { AppComponent, NgbdModalContent } from './app.component';
import { NgChartsModule } from 'ng2-charts';
import { appRoutes } from '../routes';
import { ProductModule } from './user/product/product.module';
import { AdminProductModule } from './admin/product/product.module';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { SectionSalesComponent } from './admin/report/sections/section-sales/section-sales.component';
import { SectionOrdersComponent } from './admin/report/sections/section-orders/section-orders.component';
import { SectionInventoryComponent } from './admin/report/sections/section-inventory/section-inventory.component';
import { SalesDataService } from './admin/report/services/sales-data.service';
import { BarChartComponent } from './admin/report/charts/bar-chart/bar-chart.component';
import { PieChartComponent } from './admin/report/charts/pie-chart/pie-chart.component';
import { LineChartComponent } from './admin/report/charts/line-chart/line-chart.component';
import { DateRangePickerComponent } from './admin/report/date-pickers/date-range-picker/date-range-picker.component';
import { DateMonthPickerComponent } from './admin/report/date-pickers/date-month-picker/date-month-picker.component';
import { DateYearPickerComponent } from './admin/report/date-pickers/date-year-picker/date-year-picker.component';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { SharedModule } from './shared/shared.module';
import { CartModule } from './cart/cart.module';

@NgModule({

  declarations: [
    AppComponent,
    SignupComponent,
    LoginComponent,
    HeaderComponent,
    FooterComponent,
    SectionSalesComponent,
    SectionOrdersComponent,
    SectionInventoryComponent,
    BarChartComponent,
    LineChartComponent,
    PieChartComponent,
    DateRangePickerComponent,
    DateMonthPickerComponent,
    DateYearPickerComponent,
    NgbdModalContent
  ],

  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FontAwesomeModule,
    RouterModule.forRoot(appRoutes),
    ProductModule,
    AdminProductModule,
    NgChartsModule,
    ReactiveFormsModule,
    SharedModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    FormsModule
  ],
  exports: [
    SharedModule,
    FontAwesomeModule,
    CartModule
    ReactiveFormsModule,
    FormsModule
  ],

  providers: [
    SalesDataService,
    FontAwesomeModule
  ],
  bootstrap: [
    AppComponent,
    NgbdModalContent
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
