import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UploadRequest } from './Interfaces/UploadRequest';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  @ViewChild('fileInput') uploadInput!: ElementRef;
  public uploadGroup!: FormGroup;
  title = 'BlobTask.App';
  selectedFile!: File;
  fileInvalid!: boolean;
  get email(): any {
    return this.uploadGroup.get('email')
  }

  constructor(
    private readonly fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.uploadGroup = this.fb.group({
      email: this.fb.control('', [Validators.required, Validators.email])
    });
  }

  checkFile(): boolean {
    if(this.selectedFile)
      return this.selectedFile.name.split('.').pop() == 'docx';
    
    return false;
  }

  onFileSelected(): void {
    this.selectedFile = this.uploadInput.nativeElement.files[0];
    this.fileInvalid = this.checkFile(); 
  }

  onSubmit(): void {
    this.fileInvalid = this.checkFile(); 

    if(this.fileInvalid) {
      let request: UploadRequest = {
        email: this.uploadGroup.get('email')?.value,
        file: this.selectedFile
      };

      console.log(request);
    }

       
  }
}
