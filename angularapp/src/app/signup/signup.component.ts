import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import ValidateForm from '../helpers/validateform';

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

constructor(private fb : FormBuilder){}

ngOnInit(): void{
  this.signUpForm = this.fb.group({
    email:['',Validators.required],
    password:['',Validators.required]
  })
}

  hideShow(){
    this.isText = !this.isText;
    this.isText ? this.eyeIcon = "fa-solid fa-eye" : this.eyeIcon = "fa-eye-slash";
    this.isText ? this.type = "text" : this.type = "password"
  }

  onSignUp(){
    if(this.signUpForm.valid){
      console.log(this.signUpForm.value)
    }else{
      ValidateForm.validateInput(this.signUpForm);
      alert("Your form is invalid")
    }
  }
}
