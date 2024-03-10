import { ProductCategory } from './ProductCategory';
export class Product{
  id: string;
  price: number;
  name: string;
  description: string;
  category : string;
  brandId: string;
  colorsIds: string[];
  stock: number;
  availableForPromotion: boolean;
  imageURL:string;
}
