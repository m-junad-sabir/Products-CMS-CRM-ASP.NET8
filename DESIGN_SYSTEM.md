# Products CMS - Design System & Style Guide

## Color Palette

### DaisyUI Corporate Theme Colors

#### Primary Colors
- **Primary**: `#0ea5e9` (Cyan/Sky Blue) - Main brand color
  - Used for: Links, primary buttons, headings, accents
  - Tailwind: `text-primary`, `bg-primary`, `btn-primary`

#### Neutral Colors
- **Neutral**: `#2e2e2e` (Dark Gray) - Background
  - Used for: Navbar, sidebar, card backgrounds
  - Tailwind: `bg-neutral`, `text-neutral`

- **Neutral Focus**: `#262626` (Darker Gray) - Hover state
  - Used for: Hover backgrounds, focused states
  - Tailwind: `bg-neutral-focus`, `hover:bg-neutral-focus`

- **Neutral Content**: `#cccccb` (Light Gray) - Border and secondary text
  - Used for: Borders, secondary text, dividers
  - Tailwind: `border-neutral-content`, `text-neutral-content`

#### Base Colors
- **Base 100**: `#fafafa` (White) - Main content background
  - Used for: Page background, table backgrounds
  - Tailwind: `bg-base-100`, `bg-white`

- **Base Content**: `#1f2937` (Dark Gray) - Primary text
  - Used for: Body text, headings
  - Tailwind: `text-base-content`

#### Status Colors
- **Success**: `#16a34a` (Green) - Positive actions, success states
  - Usage: Success messages, positive indicators
  - Tailwind: `badge-success`, `text-success`

- **Warning**: `#ca8a04` (Amber) - Caution, warning states
  - Usage: Warning messages, attention needed
  - Tailwind: `badge-warning`, `text-warning`

- **Error**: `#dc2626` (Red) - Errors, delete actions
  - Usage: Error messages, delete buttons
  - Tailwind: `badge-error`, `bg-error`

- **Info**: `#0284c7` (Blue) - Information, general alerts
  - Usage: Info messages, info buttons
  - Tailwind: `badge-info`, `btn-info`

## Typography

### Font Stack
```css
font-family: ui-sans-serif, system-ui, -apple-system, BlinkMacSystemFont, "Segoe UI", 
            Roboto, "Helvetica Neue", Arial, "Noto Sans", sans-serif;
```

### Heading Scale
| Level | Size | Weight | Usage |
|-------|------|--------|-------|
| H1 | 36px (2.25rem) | Bold (700) | Page titles |
| H2 | 30px (1.875rem) | Bold (700) | Section headers |
| H3 | 24px (1.5rem) | Semibold (600) | Subsections |
| H4 | 20px (1.25rem) | Semibold (600) | Minor headers |
| Body | 16px (1rem) | Normal (400) | Body text |
| Small | 14px (0.875rem) | Normal (400) | Secondary text, captions |

### Tailwind Classes
```html
<!-- Headings -->
<h1 class="text-4xl md:text-5xl font-bold">Title</h1>
<h2 class="text-3xl font-bold">Subtitle</h2>
<h3 class="text-xl font-semibold">Subheading</h3>
<h4 class="text-lg font-semibold">Minor</h4>

<!-- Body Text -->
<p class="text-base text-base-content">Regular text</p>
<p class="text-sm text-base-content/70">Secondary text</p>
```

## Button Styles

### Button Variants

#### Primary Button
```html
<a class="btn btn-primary">Primary Button</a>
```
- Background: Primary color
- Text: White
- Use for: Main actions, confirmations

#### Secondary Button (Outline)
```html
<a class="btn btn-outline btn-primary">Secondary Button</a>
```
- Background: Transparent
- Border: Primary color
- Text: Primary color
- Use for: Alternative actions

#### Gradient Buttons (with colored shadows)

**Create Button (Blue Gradient)**
```html
<button class="btn bg-gradient-to-r from-blue-500 via-blue-600 to-blue-700 
         hover:bg-gradient-to-br text-white shadow-lg shadow-blue-500/50 border-0">
    Create
</button>
```

**Edit Button (Yellow Gradient)**
```html
<button class="btn bg-gradient-to-r from-yellow-400 via-yellow-500 to-yellow-600 
         hover:bg-gradient-to-br text-white shadow-lg shadow-yellow-500/50 border-0">
    Edit
</button>
```

**Delete Button (Red Gradient)**
```html
<button class="btn bg-gradient-to-r from-red-400 via-red-500 to-red-600 
         hover:bg-gradient-to-br text-white shadow-lg shadow-red-500/50 border-0">
    Delete
</button>
```

#### Ghost Button
```html
<button class="btn btn-ghost">Ghost Button</button>
```
- Background: Transparent
- Text: Primary color
- Use for: Tertiary actions, dismissible actions

### Button Sizes
```html
<button class="btn btn-xs">Extra Small</button>
<button class="btn btn-sm">Small</button>
<button class="btn">Default (md)</button>
<button class="btn btn-lg">Large</button>
```

## Component Patterns

### Card Component
```html
<div class="card bg-neutral border border-neutral-content shadow-lg hover:shadow-xl transition-shadow">
    <div class="card-body">
        <h3 class="card-title">Card Title</h3>
        <p>Card content goes here</p>
    </div>
</div>
```

### Badge Component
```html
<!-- Success Badge -->
<span class="badge badge-success">Success</span>

<!-- Warning Badge -->
<span class="badge badge-warning">Warning</span>

<!-- Error Badge -->
<span class="badge badge-error">Error</span>

<!-- Info Badge -->
<span class="badge badge-info">Info</span>
```

### Table Component
```html
<div class="overflow-x-auto bg-base-100 rounded-lg border border-base-300 shadow-lg">
    <table class="w-full table table-zebra">
        <thead class="bg-neutral border-b border-base-300">
            <tr>
                <th>Header 1</th>
                <th>Header 2</th>
            </tr>
        </thead>
        <tbody class="divide-y divide-base-300">
            <tr class="hover:bg-neutral transition-colors">
                <td>Data 1</td>
                <td>Data 2</td>
            </tr>
        </tbody>
    </table>
</div>
```

### Form Input
```html
<input type="text" placeholder="Enter text..." class="input input-bordered w-full" />
<textarea placeholder="Enter text..." class="textarea textarea-bordered w-full"></textarea>
<select class="select select-bordered w-full">
    <option>Option 1</option>
</select>
```

## Spacing System

### Margin & Padding Scale
| Scale | Value | Tailwind |
|-------|-------|----------|
| xs | 0.25rem (4px) | `p-1`, `m-1` |
| sm | 0.5rem (8px) | `p-2`, `m-2` |
| md | 1rem (16px) | `p-4`, `m-4` |
| lg | 1.5rem (24px) | `p-6`, `m-6` |
| xl | 2rem (32px) | `p-8`, `m-8` |
| 2xl | 3rem (48px) | `p-12`, `m-12` |

### Common Patterns
```html
<!-- Vertical spacing -->
<div class="space-y-4">Content</div>

<!-- Horizontal spacing -->
<div class="space-x-4">Content</div>

<!-- Section padding -->
<section class="py-12 md:py-20">Content</section>
```

## Layout Patterns

### Container Width
```html
<div class="max-w-screen-xl mx-auto px-4">
    <!-- Content constrained to max width with responsive padding -->
</div>
```

### Responsive Grid
```html
<!-- 1 column on mobile, 2 on tablet, 3+ on desktop -->
<div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
    <div>Item 1</div>
    <div>Item 2</div>
    <div>Item 3</div>
</div>
```

### Flex Layout
```html
<div class="flex flex-col md:flex-row items-center justify-between gap-4">
    <div>Left content</div>
    <div>Right content</div>
</div>
```

## Responsive Breakpoints

| Breakpoint | Min Width | Usage |
|-----------|-----------|-------|
| (none) | 0px | Mobile first |
| sm | 640px | Small tablets |
| md | 768px | Tablets |
| lg | 1024px | Desktops |
| xl | 1280px | Large desktops |
| 2xl | 1536px | Extra large screens |

### Usage in Classes
```html
<!-- Mobile (default): flex, Tablet (md): grid -->
<div class="flex md:grid">Content</div>

<!-- Hidden on mobile, visible on md+ -->
<div class="hidden md:flex">Content</div>

<!-- Adjust padding for responsive -->
<div class="px-4 md:px-0">Content</div>
```

## Shadows

### Shadow Scale
```html
<!-- No shadow -->
<div class="shadow-none">Content</div>

<!-- Small shadow -->
<div class="shadow-sm">Content</div>

<!-- Medium shadow (default) -->
<div class="shadow">Content</div>

<!-- Large shadow -->
<div class="shadow-lg">Content</div>

<!-- Extra large shadow -->
<div class="shadow-xl">Content</div>
```

### Colored Shadows
```html
<!-- Blue colored shadow -->
<div class="shadow-lg shadow-blue-500/50">Content</div>

<!-- Red colored shadow -->
<div class="shadow-lg shadow-red-500/50">Content</div>

<!-- Primary colored shadow -->
<div class="shadow-lg shadow-primary/50">Content</div>
```

## Transitions & Animations

### Transition Classes
```html
<!-- Color transitions -->
<a class="text-base-content hover:text-primary transition-colors">Link</a>

<!-- Shadow transitions -->
<div class="shadow-lg hover:shadow-xl transition-shadow">Card</div>

<!-- Multiple transitions -->
<button class="transition-all hover:scale-105 hover:shadow-xl">Button</button>
```

## Accessibility

### Color Contrast
- Main text on main background: WCAG AA compliant
- Secondary text: Uses opacity (`text-base-content/70`)
- Interactive elements: Have focus indicators

### Keyboard Navigation
```html
<!-- Focus ring -->
<button class="focus:ring-4 focus:ring-primary focus:outline-none">Button</button>

<!-- Screen reader only -->
<span class="sr-only">Only visible to screen readers</span>
```

### ARIA Labels
```html
<button aria-label="Open menu">??</button>
<input aria-label="Search products" />
```

## Usage Examples

### Hero Section
```html
<section class="py-12 md:py-20">
    <div class="grid grid-cols-1 md:grid-cols-2 gap-8 items-center">
        <div>
            <h1 class="text-4xl md:text-5xl font-bold text-primary mb-4">
                Welcome
            </h1>
            <p class="text-lg text-base-content/80 mb-6">
                Description text here
            </p>
            <a class="btn btn-primary">Call to Action</a>
        </div>
    </div>
</section>
```

### Feature Card Section
```html
<section class="py-12">
    <h2 class="text-3xl font-bold text-center text-primary mb-12">Features</h2>
    <div class="grid grid-cols-1 md:grid-cols-3 gap-6">
        <div class="card bg-neutral border border-neutral-content shadow-lg hover:shadow-xl transition-shadow">
            <div class="card-body">
                <h3 class="card-title">Feature 1</h3>
                <p>Description</p>
            </div>
        </div>
    </div>
</section>
```

## Theme Switching

To switch DaisyUI themes, change the `data-theme` attribute:

```html
<!-- HTML tag -->
<html lang="en" data-theme="corporate">

<!-- Or via JavaScript -->
<script>
    document.documentElement.setAttribute('data-theme', 'dark');
</script>
```

## Best Practices

1. **Consistency**: Use the defined color palette consistently
2. **Hierarchy**: Use clear visual hierarchy with size and weight
3. **Spacing**: Maintain consistent spacing using the scale
4. **Accessibility**: Always include alt text, labels, and ARIA attributes
5. **Performance**: Leverage Tailwind's class-based approach
6. **Responsiveness**: Always test on mobile, tablet, and desktop
7. **Contrast**: Ensure sufficient color contrast for readability
8. **Feedback**: Provide visual feedback for interactive elements (hover, focus, active states)

---

**Version**: 1.0  
**Last Updated**: 2025  
**Framework**: Tailwind CSS v3 + DaisyUI v4 + Flowbite v2
