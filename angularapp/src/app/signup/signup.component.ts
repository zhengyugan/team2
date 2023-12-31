import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import ValidateForm from '../helpers/validateform';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'pm-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent {
  type: string ="password";
  isText: boolean = false;
  eyeIcon: string = "fa-eye-slash";
  signUpForm!: FormGroup;

constructor(private fb : FormBuilder, private auth: AuthService, private router: Router, private toast: NgToastService){}

ngOnInit(): void{
  this.signUpForm = this.fb.group({
    email:['',Validators.required],
    password:['',Validators.required],
    firstname:['',Validators.required],
    lastname:['',Validators.required]
  })
}

  hideShow(){
    this.isText = !this.isText;
    this.isText ? this.eyeIcon = "fa-solid fa-eye" : this.eyeIcon = "fa-eye-slash";
    this.isText ? this.type = "text" : this.type = "password"
  }
  cancel(){
    this.router.navigate(['products']);
  }

  onSignUp(){
    if(this.signUpForm.valid){
      console.log(this.signUpForm.value)
      this.auth.signUp(this.signUpForm.value)
      .subscribe({
        next:(res)=>{
          // alert(res.message)
          this.toast.success({detail:"SUCCESS", summary:res.message, duration: 5000});
          this.signUpForm.reset();
          this.router.navigate(['login']);
        },
        error:(err)=>{
          alert(err?.error.message)
        }
      })
    }else{
      ValidateForm.validateInput(this.signUpForm);
      // alert()
      this.toast.error({detail:"ERROR", summary:"Your form is invalid", duration: 5000})

    }
  }
}
