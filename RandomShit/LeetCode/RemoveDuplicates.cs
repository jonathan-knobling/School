namespace RandomShit.LeetCode;

public static class RemoveDuplicates
{
    public static int Remove(int[] nums)
    {
        int p1 = 0, p2 = 0;
        int last = int.MinValue;

        while (p2 < nums.Length)
        {
            if (nums[p2] > last)
            {
                last = nums[p2];
                nums[p1] = last;
                p1++;
            }

            p2++;
        }

        return p1;
    }
}