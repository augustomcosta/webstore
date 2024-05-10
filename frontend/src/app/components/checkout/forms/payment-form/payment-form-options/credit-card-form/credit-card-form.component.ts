import { Component, EventEmitter, inject, OnInit, Output } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { CreditCardBrand } from '../../../../../../shared/enum/credit-card-brands';
import { NgOptimizedImage } from '@angular/common';

@Component({
  selector: 'app-credit-card-form',
  standalone: true,
  imports: [ReactiveFormsModule, NgOptimizedImage],
  templateUrl: './credit-card-form.component.html',
  styleUrl: './credit-card-form.component.css',
})
export class CreditCardFormComponent implements OnInit {
  creditCardForm!: FormGroup;
  fb = inject(FormBuilder);
  http = inject(HttpClient);
  @Output() formValidityChanged: EventEmitter<boolean> =
    new EventEmitter<boolean>();
  cardBrand: CreditCardBrand | undefined;
  cardBrandImages: { [key in CreditCardBrand]: string };

  constructor() {
    this.cardBrandImages = {
      [CreditCardBrand.mastercard]:
        'https://brand.mastercard.com/content/dam/mccom/brandcenter/thumbnails/mastercard_vrt_rev_92px_2x.png',
      [CreditCardBrand.visa]:
        'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSol5MKguow8RjUenCB75Y8VND273RsP6FXyAxbt9dhjg&s',
      [CreditCardBrand.amex]:
        'https://w7.pngwing.com/pngs/1002/997/png-transparent-amex-payment-method-card-icon-thumbnail.png',
      [CreditCardBrand.discover]: 'https://example.com/discover.png',
      [CreditCardBrand.unknown]: 'https://example.com/unknown.png',
      [CreditCardBrand.dinersclub]: 'https://example.com/dinersclub.png',
      [CreditCardBrand.jcb]: 'https://example.com/jcb.png',
      [CreditCardBrand.elo]: 'https://example.com/elo.png',
      [CreditCardBrand.aura]: 'https://example.com/aura.png',
      [CreditCardBrand.hipercard]: 'https://example.com/hipercard.png',
      [CreditCardBrand.enroute]: 'https://example.com/enroute.png',
    };
  }

  ngOnInit(): void {
    this.creditCardForm = this.fb.group({
      cardNumber: ['', Validators.required],
      CVC: ['', Validators.required],
      expiryDate: ['', Validators.required],
      nameOnCard: ['', Validators.required],
    });

    this.creditCardForm.valueChanges.subscribe(() => {
      this.formValidityChanged.emit(this.creditCardForm.valid);
    });

    this.creditCardForm.get('cardNumber')!.valueChanges.subscribe(() => {
      this.cardSelection();
    });
  }

  cardSelection() {
    const cardNumber: string = this.creditCardForm.get('cardNumber')!.value;

    if (
      cardNumber.match(
        /^((5[1-5])|(222[1-9]|22[3-9][0-9]|2[3-6][0-9]{2}|27[01][0-9]|2720))/,
      )
    ) {
      this.cardBrand = CreditCardBrand.mastercard;
    } else if (cardNumber.startsWith('4')) {
      this.cardBrand = CreditCardBrand.visa;
    } else if (cardNumber.match(/^((34)|(37))/)) {
      this.cardBrand = CreditCardBrand.amex;
    } else if (cardNumber.match(/^((6[45])|(6011))/)) {
      this.cardBrand = CreditCardBrand.discover;
    } else if (cardNumber.match(/^(3841[046]0|60)/)) {
      this.cardBrand = CreditCardBrand.hipercard;
    } else if (cardNumber.match(/^((30[0-5])|(3[89])|(36)|(3095))/)) {
      this.cardBrand = CreditCardBrand.dinersclub;
    } else if (cardNumber.match(/^(352[89]|35[3-8][0-9])/)) {
      this.cardBrand = CreditCardBrand.jcb;
    } else if (cardNumber.startsWith('50')) {
      this.cardBrand = CreditCardBrand.aura;
    } else {
      this.cardBrand = CreditCardBrand.unknown;
    }
    return this.cardBrand;
  }
}
