import { User } from './user';

export interface Order {
  id: number;
  users: User;
  total: number;
  created_at: Date;
  moodified_at: Date;
  order_status: string;
}
