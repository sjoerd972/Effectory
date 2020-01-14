import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Question, AnswerOption } from './../model';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.sass']
})

export class DetailComponent implements OnInit {
  surveyUrl = 'https://localhost:44380/survey';
  questionUrl = 'https://localhost:44380/question';
  questions: Question[];

  constructor(private http: HttpClient, private route: ActivatedRoute) { }

  ngOnInit(): void {
    let surveyId;

    this.route.queryParams.subscribe(params => {
      surveyId = params['surveyId'];
    });

    const params = new HttpParams().set('surveyId', surveyId);

    this.http.get<Question[]>(this.questionUrl, { params }).subscribe(data => {
      let answerOptions = Array<AnswerOption>();
      this.questions = data.map(d => new Question(d["id"], d["title"], answerOptions));

      console.log(this.questions);
    });
  }
}
