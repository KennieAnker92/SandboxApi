import { test, expect } from '@playwright/test';

test.describe('SandboxApi Home Page', () => {

  test.beforeEach(async ({ page }) => {
    await page.goto('/');
  });

  test('Full-stack health check and UI load', async ({ page, request }) => {
    // 1. Verify the C# Backend is responding
    const apiResponse = await request.get('http://localhost:5011/openapi/v1.json');
    expect(apiResponse.status()).toBe(200);

    // 2. Verify the Vite React frontend loads
    await page.goto('/');
    await expect(page.locator('#root')).toBeVisible();
  });

  test('Hero scroll arrow is visible and clickable', async ({ page }) => {
    const scrollBtn = page.locator('.hero-scroll-btn');
    
    // Verify the button renders
    await expect(scrollBtn).toBeVisible();
    
    // Verify it can be clicked without throwing an error or breaking the page
    await scrollBtn.click(); 
    
    await expect(page.locator('.featured-section')).toBeVisible();
  });

  test('test', async ({ page }) => {
  await page.goto('http://localhost:5173/');
  await page.getByRole('button', { name: 'Explore Catalog' }).click();
  await page.getByText('View Details ›').nth(1).click();
  await page.getByRole('button', { name: '← Back to Products' }).click();
});
})