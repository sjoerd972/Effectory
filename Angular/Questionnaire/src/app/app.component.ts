import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Survey } from './model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass'],
})

export class AppComponent implements OnInit {
  surveyUrl = 'https://localhost:44380/survey';
  surveys: Survey[];

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.http.get<Survey[]>(this.surveyUrl).subscribe(data => {
      this.surveys = data.map(d => new Survey(d["id"], d["title"]));

      console.log(this.surveys);
    });
  }
}
