import { useEffect, useState } from 'react';

import './ProductDetails.css';
import type { Product } from '../../api/API';
import { api } from '../../api/client';

interface Props {
  productId: number;
  onBack: () => void;
}

export function ProductDetails({ productId, onBack }: Props) {
  const [product, setProduct] = useState<Product | null>(null);

  useEffect(() => {
    async function loadProduct() {
      const res = await api.api.productsDetail(productId);
      setProduct(res.data);
    }
    loadProduct();
  }, [productId]);

  if (!product) return <div className="loading">Loading product details...</div>;

  return (
    <div className="product-details-page">
      <button className="back-btn" onClick={onBack}>← Back to Products</button>
      <div className="product-details-container">
        <h2 className="product-title">{product.name}</h2>
        <p className="product-price">${product.price}</p>
        <p className="product-description-full">{product.description}</p>
      </div>
    </div>
  );
}