export enum CreditCardBrand {
  unknown = 0,
  mastercard = 1,
  visa = 2,
  amex = 3,
  discover = 4,
  dinersclub = 5,
  jcb = 6,
  elo = 8,
  aura = 9,
  hipercard = 10,
  enroute = 7,
}

export const cardBrandImages = {
  [CreditCardBrand.mastercard]:
    'https://brand.mastercard.com/content/dam/mccom/brandcenter/thumbnails/mastercard_vrt_rev_92px_2x.png',
  [CreditCardBrand.visa]:
    'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSol5MKguow8RjUenCB75Y8VND273RsP6FXyAxbt9dhjg&s',
  [CreditCardBrand.amex]:
    'https://w7.pngwing.com/pngs/1002/997/png-transparent-amex-payment-method-card-icon-thumbnail.png',
};
