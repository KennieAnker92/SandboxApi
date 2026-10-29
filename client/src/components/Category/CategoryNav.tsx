import type { Category } from '../../api/API';
import './CategoryNav.css';

interface Props {
  categories: Category[];
  selectedId: number | null;
  onSelect: (id: number | null) => void;
}

export function CategoryNav({ categories, selectedId, onSelect }: Props) {
  return (
    <nav className="category-nav">
      <button 
        className={`category-btn ${selectedId === null ? 'active' : ''}`}
        onClick={() => onSelect(null)}
      >
        All Products
      </button>
      {categories.map(cat => (
        <button 
          key={cat.id} 
          className={`category-btn ${selectedId === cat.id ? 'active' : ''}`}
          onClick={() => cat.id && onSelect(cat.id)}
        >
          {cat.name}
        </button>
      ))}
    </nav>
  );
}