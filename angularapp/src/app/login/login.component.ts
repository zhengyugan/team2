import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import ValidateForm from '../helpers/validateform';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'pm-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  type: string ="password";
  isText: boolean = false;
  eyeIcon: string = "fa-eye-slash";
  loginForm!: FormGroup;
  constructor(private fb: FormBuilder, private auth: AuthService, private router: Router, private toast: NgToastService){}

  ngOnInit(): void{
    this.loginForm = this.fb.group({
      email:['',Validators.required],
      password:['',Validators.required]
    })
  }

  hideShow(){
    this.isText = !this.isText;
    this.isText ? this.eyeIcon = "fa-solid fa-eye" : this.eyeIcon = "fa-eye-slash";
    this.isText ? this.type = "text" : this.type = "password"
  }

  onLogin(){
    if(this.loginForm.valid){
      console.log(this.loginForm.value)
      this.auth.login(this.loginForm.value)
      .subscribe({
        next:(res)=>{
          // alert(res.message);
          this.loginForm.reset();
          this.auth.storeToken(res.login)
          this.auth.storeRole(res.role)
          console.log(res.role);
          this.toast.success({detail:"SUCCESS", summary:res.message, duration: 5000});
          if(res.role === 'admin'){
            this.router.navigate(['dashboard'])
          }else{
            this.router.navigate(['products'])
          }
        },
        error:(err)=>{
          // alert(err?.error.message)
          this.toast.error({detail:"ERROR", summary:"Something went wrong!", duration: 5000})
        }
      })
    }else{
      ValidateForm.validateInput(this.loginForm);
      alert()
      this.toast.error({detail:"ERROR", summary:"Your form is invalid", duration: 5000})

    }
  }

  logOut(){
    this.router.navigate(['products'])
  }

  cancel(){
    this.router.navigate(['products']);
  }

  
}
