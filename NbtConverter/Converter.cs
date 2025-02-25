using fNbt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fNbt;

namespace NbtConverter
{
    internal class Converter
    {

        internal static void ConvertToMinecraftNbt(string _in,string _out)
        {

            var file = new NbtFile();

            file.LoadFromFile(_in);

            var root = file.RootTag;

            var blocks = root.Get<NbtIntArray>("blocks");

            var old_block_list = new List<int>();

            var size_x = root.Get<NbtShort>("size_x").IntValue;
            var size_y = root.Get<NbtShort>("size_y").IntValue;
            var size_z = root.Get<NbtShort>("size_z").IntValue;


            var size_tag = new NbtList("size")
            {
                new NbtInt(size_x),
                new NbtInt(size_y),
                new NbtInt(size_z)
            };

            foreach (var block in blocks.IntArrayValue)
            {

                int b1 = (short)block;
                int b2 = (block >> 16);

                old_block_list.Add(b2);
                old_block_list.Add(b1);

            }

            var block_list = new NbtList("blocks");

            int block_index = 0;

            for (int y = 0; y < size_y; y++)
            {
                for (int z = 0; z < size_z; z++)
                {
                    for (int x = 0; x < size_x; x++)
                    {
                        var new_block = new NbtCompound();

                        new_block.Add(new NbtInt("state", old_block_list[block_index]));

                        var pos_list = new NbtList("pos")
                        {
                            new NbtInt(x),
                            new NbtInt(y),
                            new NbtInt(z)
                        };

                        new_block.Add(pos_list);

                        block_list.Add(new_block);

                        block_index++;
                    }
                }
            }

            root.Remove("blocks");
            root.Add(block_list);
            root.Add(size_tag);

            file.RootTag = root;

            file.SaveToFile(_out, NbtCompression.GZip);

        }
    }
}
