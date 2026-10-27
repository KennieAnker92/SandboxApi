import type { Product } from '../../api/API';
import './ProductCard.css';

interface Props {
  product: Product;
  onClick: () => void;
}

export function ProductCard({ product, onClick }: Props) {
  return (
    <div className="product-card" onClick={onClick}>
      <h3 className="product-title">{product.name}</h3>
      <p className="product-price">${product.price}</p>
      <p className="product-description">{product.description}</p>
    </div>
  );
}