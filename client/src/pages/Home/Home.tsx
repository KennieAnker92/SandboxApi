import { useEffect, useState } from 'react';
import './Home.css';
import type { Product, Category } from '../../api/API';
import { api } from '../../api/client';
import { CategoryNav } from '../../components/Category/CategoryNav';
import { Hero } from '../../components/Hero/Hero';
import { ProductCard } from '../../components/Product/ProductCard';
import { FeaturedProducts } from '../Product/FeaturedProducts';

interface Props {
  onSelectProduct: (id: number) => void;
}

export function Home({ onSelectProduct }: Props) {
  const [products, setProducts] = useState<Product[]>([]);
  const [featuredProducts, setFeaturedProducts] = useState<Product[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);
  const [selectedCategory, setSelectedCategory] = useState<number | null>(null);

  useEffect(() => {
    async function loadInitialData() {
      const catRes = await api.api.categoriesList();
      setCategories(catRes.data || []);

      const prodRes = await api.api.productsList();
      const allProducts = prodRes.data || [];
      setFeaturedProducts(allProducts.slice(0, 3));
    }
    loadInitialData();
  }, []);

  useEffect(() => {
    async function loadProducts() {
      const res = selectedCategory !== null
        ? await api.api.productsList({ categoryId: selectedCategory })
        : await api.api.productsList();
      setProducts(res.data || []);
    }
    loadProducts();
  }, [selectedCategory]);

  const handleProductClick = (id: number) => {
    onSelectProduct(id);
  };

  return (
    <div className="home-page">
      <Hero />
      <FeaturedProducts 
        products={featuredProducts} 
        onProductClick={handleProductClick} 
      />
      <CategoryNav 
        className="category-nav"
        categories={categories} 
        selectedId={selectedCategory} 
        onSelect={setSelectedCategory} 
      />
      <div className="product-grid">
        {products.map(product => (
          <ProductCard 
            key={product.id} 
            product={product} 
            onClick={() => product.id && onSelectProduct(product.id)} 
          />
        ))}
      </div>
    </div>
  );
}