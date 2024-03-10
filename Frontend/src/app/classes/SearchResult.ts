import { Brand } from "./Brand";
import { ProductCategory } from "./ProductCategory";
import { ProductInformation } from "./ProductInformation";

export class SearchResult{
  brandName: string;
  productName: string;
  category: ProductCategory;
  price: number;
  productId: string;
  imageURL:string;
}
