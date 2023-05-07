export interface MenuItem {
  id?: number;
  label?: string;
  icon?: string;
  link?: string;
  subItems?: any;
  parentId?: number;
  isTopbarMenu?: boolean;
  description?: string;
  isHovered?: boolean;
}
