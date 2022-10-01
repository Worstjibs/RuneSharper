import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { LineChartComponent } from './line-chart/line-chart.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { HttpClientModule } from '@angular/common/http';
import { HomeComponent } from './home/home.component';
import { NavComponent } from './nav/nav.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { AppRoutingModule } from './app-routing.module';
import { CharacterListComponent } from './character-list/character-list.component';
import { NgxDatatableModule } from '@boring.devs/ngx-datatable';
import { DatePipe } from '@angular/common';

@NgModule({
  declarations: [
    AppComponent,
    LineChartComponent,
    HomeComponent,
    NavComponent,
    CharacterListComponent
  ],
  imports: [
    BsDropdownModule.forRoot(),
    BrowserModule,
    BrowserAnimationsModule,
    NgxChartsModule,
    NgxDatatableModule,
    HttpClientModule,
    AppRoutingModule    
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
