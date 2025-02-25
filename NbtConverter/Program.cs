using System.Collections.Generic;
using System.Collections.Immutable;
using fNbt;

//var path = Console.ReadLine();


var path = "C:\\My Stuff\\NBT Stuff\\caledonia\\agriculture\\horticulture\\alfarmer5.nbt";

var file = new NbtFile();

file.LoadFromFile(path);

var root = file.RootTag;

var blocks = root.Get<NbtIntArray>("blocks");

var old_block_list = new List<int>();

var size_x = root.Get<NbtShort>("size_x").IntValue;
var size_y = root.Get<NbtShort>("size_y").IntValue;
var size_z = root.Get<NbtShort>("size_z").IntValue;


var size_tag = new NbtList("size");
size_tag.Add(new NbtInt(size_x));
size_tag.Add(new NbtInt(size_y));
size_tag.Add(new NbtInt(size_z));

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
               
            var pos_list = new NbtList("pos"); 

            pos_list.Add(new NbtInt(x));
            pos_list.Add(new NbtInt(y));
            pos_list.Add(new NbtInt(z));

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

Console.WriteLine("Output file path");
var out_path = Console.ReadLine();

file.SaveToFile(out_path, NbtCompression.GZip);
