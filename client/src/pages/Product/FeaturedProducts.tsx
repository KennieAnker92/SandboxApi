
import type { Product } from '../../api/API';
import './FeaturedProducts.css';

interface Props {
  products: Product[];
  onProductClick: (id: number) => void;
}

export function FeaturedProducts({ products, onProductClick }: Props) {
  const featured = products.slice(0, 3);

  if (featured.length === 0) return null;

  return (
    <section className="featured-section">
      <div className="featured-header">
        <h2>Featured Innovations</h2>
        <p>Hand-picked solutions engineered for maximum performance.</p>
      </div>
      
      <div className="featured-grid">
        {featured.map((product) => (
          <div 
            key={product.id} 
            className="featured-card"
            onClick={() => onProductClick(product.id!)}
          >
            <div className="featured-card-visual">
               <div className="featured-visual-placeholder"></div>
            </div>
            <div className="featured-card-content">
              <h3 className="featured-card-title">{product.name}</h3>
              <p className="featured-card-desc">{product.description}</p>
              <div className="featured-card-footer">
                <span className="featured-card-price">${product.price}</span>
                <span className="featured-card-action">View Details ›</span>
              </div>
            </div>
          </div>
        ))}
      </div>
    </section>
  );
}