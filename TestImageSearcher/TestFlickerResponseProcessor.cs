using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ImageSearcher;


namespace TestImageSearcher
{
    [TestFixture]
    public class TestFlickerResponseProcessor
    {
        FlickerResponseProcessor processor;

        string sampleXml = @"<rsp stat=""ok"">
	<photos page=""2"" pages=""1389"" perpage=""100"" total=""138828"">
		<photo id=""52563028656"" owner=""149200955@N05"" secret=""b4989029eb"" server=""65535"" farm=""66"" title=""selkirk mountains"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563307389"" owner=""82256086@N00"" secret=""a6f755901b"" server=""65535"" farm=""66"" title=""White Mountain trees N.H. autumn fall seasons colors landscape"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563483790"" owner=""76856785@N00"" secret=""52be1246a3"" server=""65535"" farm=""66"" title=""Last rays..."" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563543053"" owner=""80683734@N00"" secret=""71d7e445b2"" server=""65535"" farm=""66"" title=""A Taos Neighborhood"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563004531"" owner=""76093270@N04"" secret=""df9de58b45"" server=""65535"" farm=""66"" title=""『Rebuilt 5th Bridge』"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52562502797"" owner=""63659356@N07"" secret=""05c220b61d"" server=""65535"" farm=""66"" title=""Lackawanna at Mountain Lakes"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52562518877"" owner=""151111185@N04"" secret=""1a7fd2b3ef"" server=""65535"" farm=""66"" title=""The Last Chase"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52562500472"" owner=""38298328@N08"" secret=""94f6197f53"" server=""65535"" farm=""66"" title=""Aleppo Pepper 40g £2.20p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563419140"" owner=""38298328@N08"" secret=""94bac7544a"" server=""65535"" farm=""66"" title=""Aleppo Pepper 40g £2.30p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52562500127"" owner=""38298328@N08"" secret=""b157ab122c"" server=""65535"" farm=""66"" title=""Amchur (Mango Powder) 50g £1.80p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52562954901"" owner=""38298328@N08"" secret=""48def054a8"" server=""65535"" farm=""66"" title=""Baharat 40g £2.40p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563238364"" owner=""38298328@N08"" secret=""77132ef7fa"" server=""65535"" farm=""66"" title=""Balti Curry 40g £2.30p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563491883"" owner=""38298328@N08"" secret=""5c0107ffee"" server=""65535"" farm=""66"" title=""Biryani 40g £2.40p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563491748"" owner=""38298328@N08"" secret=""7ee1251a0d"" server=""65535"" farm=""66"" title=""Blue Cornflower 5g £2's, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563237704"" owner=""38298328@N08"" secret=""2efe8a51d7"" server=""65535"" farm=""66"" title=""Bombay Potato 50g £2.40p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563237269"" owner=""38298328@N08"" secret=""2dd2600664"" server=""65535"" farm=""66"" title=""Carolina Reaper Fine Powder (Worlds HOTTEST Chilli) 10g £6's, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52562498182"" owner=""38298328@N08"" secret=""c38f316972"" server=""65535"" farm=""66"" title=""Carolina Reaper Paste (Worlds HOTTEST Chilli) 100g £6's, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563490638"" owner=""38298328@N08"" secret=""669efdcf58"" server=""65535"" farm=""66"" title=""Ceylon Cinnamon 60g £3.70p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563416545"" owner=""38298328@N08"" secret=""9388780ea7"" server=""65535"" farm=""66"" title=""Chia Seeds 80g £2.30p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563236139"" owner=""38298328@N08"" secret=""aa2a2ae0b8"" server=""65535"" farm=""66"" title=""Chickpea Curry or Dal 40g £2.40p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52562497147"" owner=""38298328@N08"" secret=""65cf5f4e01"" server=""65535"" farm=""66"" title=""Chili Pyramid Salt Flakes 70g £3.70p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52562951856"" owner=""38298328@N08"" secret=""afa555709a"" server=""65535"" farm=""66"" title=""Chilli Salt 100g £2.30p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH (1)"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563489048"" owner=""38298328@N08"" secret=""8d6a11da7f"" server=""65535"" farm=""66"" title=""Chinese Five Spice 40g £2.30p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563235249"" owner=""38298328@N08"" secret=""09b80fccf5"" server=""65535"" farm=""66"" title=""Chipotle Powder 40g £2.50p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563234979"" owner=""38298328@N08"" secret=""299cd2f7e3"" server=""65535"" farm=""66"" title=""Coffee Spice 30g £2's, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563229404"" owner=""65778609@N03"" secret=""c8e7c7d265"" server=""0"" farm=""0"" title=""Cordillera de la Sal, San Pedro de Atacama, Chile"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563414870"" owner=""38298328@N08"" secret=""ea9e4e32ba"" server=""65535"" farm=""66"" title=""Cranberries 60g £3's, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563234669"" owner=""38298328@N08"" secret=""5880aaf5c7"" server=""65535"" farm=""66"" title=""Crunchy Tomato Slices with Oregano and Basil 30g £2.80p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52562495317"" owner=""38298328@N08"" secret=""ac1ec1d7a1"" server=""65535"" farm=""66"" title=""Dessicated Coconut 50g £1.40p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52562950146"" owner=""38298328@N08"" secret=""3e7709951e"" server=""65535"" farm=""66"" title=""Dukkak 40g £2.40p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH (2)"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52562949991"" owner=""38298328@N08"" secret=""abf19041b5"" server=""65535"" farm=""66"" title=""Falafel Spice 40g £2.30p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52562494832"" owner=""38298328@N08"" secret=""7655de0a67"" server=""65535"" farm=""66"" title=""French Type Lentils (Sourced Organically, Pur Lentils) 300g £2.30p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563233479"" owner=""38298328@N08"" secret=""b941c63ac6"" server=""65535"" farm=""66"" title=""Furikake Japanese Rice 50g £2.40p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563487183"" owner=""13905539@N06"" secret=""c28b605df2"" server=""65535"" farm=""66"" title=""Lac Duchesnay, BC"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52562949406"" owner=""38298328@N08"" secret=""8909642ddf"" server=""65535"" farm=""66"" title=""Garam Masala 100g £4's, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563413260"" owner=""38298328@N08"" secret=""e05ab11a79"" server=""65535"" farm=""66"" title=""Green Jalapeno 40g £2.20p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563232689"" owner=""38298328@N08"" secret=""434c733b0e"" server=""65535"" farm=""66"" title=""Habanero Powder 30g £2.60p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563412670"" owner=""38298328@N08"" secret=""4e405903a7"" server=""65535"" farm=""66"" title=""Hamaiian Bamboo Jade Salt 80g £3.30p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563485913"" owner=""38298328@N08"" secret=""8d817e89c2"" server=""65535"" farm=""66"" title=""Harrissa Spice 40g £2.40p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52562948046"" owner=""38298328@N08"" secret=""b45e83fb8b"" server=""65535"" farm=""66"" title=""Hawaiian Red Alaea Salt 50g £2.60p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563411900"" owner=""38298328@N08"" secret=""883fd6956a"" server=""65535"" farm=""66"" title=""Hickery Smoked Salt 90g £4's, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52562947571"" owner=""38298328@N08"" secret=""858104427d"" server=""65535"" farm=""66"" title=""Hot Chilli Flakes 30g £2's, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH (2)"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52562947391"" owner=""38298328@N08"" secret=""f2f5a51ce5"" server=""65535"" farm=""66"" title=""Hot Chilli Flakes 30g £2's, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563231169"" owner=""38298328@N08"" secret=""a8ec02f631"" server=""65535"" farm=""66"" title=""Hot Madras 40g £2.30p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52562947101"" owner=""38298328@N08"" secret=""736006fe44"" server=""65535"" farm=""66"" title=""Hot Smoked Paprika 40g £2.10p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563410780"" owner=""38298328@N08"" secret=""a6cb74fb15"" server=""65535"" farm=""66"" title=""Hungarian Paprika 40g £2.20p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563230584"" owner=""38298328@N08"" secret=""328df5205e"" server=""65535"" farm=""66"" title=""Japanese Gomashio 50g £2.30p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563483993"" owner=""65778609@N03"" secret=""c0ab4490d2"" server=""65535"" farm=""66"" title=""Cordillera de la Sal, San Pedro de Atacama, Chile"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52562491017"" owner=""38298328@N08"" secret=""deceb9310f"" server=""65535"" farm=""66"" title=""Japanese Matcha 25g £6.80p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563483793"" owner=""38298328@N08"" secret=""dd6a7e6354"" server=""65535"" farm=""66"" title=""Kaffir Lime Leaves 6g £2's, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563410120"" owner=""65778609@N03"" secret=""5b8434afb5"" server=""65535"" farm=""66"" title=""Cordillera de la Sal, San Pedro de Atacama, Chile"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563483578"" owner=""65778609@N03"" secret=""197cf4fef8"" server=""65535"" farm=""66"" title=""Cordillera de la Sal, San Pedro de Atacama, Chile"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563229764"" owner=""65778609@N03"" secret=""1c16f14ac9"" server=""65535"" farm=""66"" title=""Cordillera de la Sal, San Pedro de Atacama, Chile"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563483333"" owner=""38298328@N08"" secret=""7f4548771d"" server=""65535"" farm=""66"" title=""Karachi Curry 40g £2.40p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563229089"" owner=""38298328@N08"" secret=""75ae43e0bc"" server=""65535"" farm=""66"" title=""Korean Chilli Flakes 40g £3.60p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563228749"" owner=""38298328@N08"" secret=""dfa1fff851"" server=""65535"" farm=""66"" title=""Korean Roasted Bamboo Salt (Jukyeom) 100g £2.80p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563408540"" owner=""38298328@N08"" secret=""9d8d571872"" server=""65535"" farm=""66"" title=""Korma 40g £2.30p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563408350"" owner=""38298328@N08"" secret=""30b2eb42d1"" server=""65535"" farm=""66"" title=""Korma 40g £2.40g, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52562943946"" owner=""38298328@N08"" secret=""3e96d2ca2c"" server=""65535"" farm=""66"" title=""Lavender Salt 40g £3's, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563408005"" owner=""38298328@N08"" secret=""ff440e0539"" server=""65535"" farm=""66"" title=""Lebanese Kofte 40g £2.30, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52562488212"" owner=""38298328@N08"" secret=""6ca9c6d310"" server=""65535"" farm=""66"" title=""Lebanese Shawarma 40g £2.50p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52562487827"" owner=""38298328@N08"" secret=""ab845f810c"" server=""65535"" farm=""66"" title=""Lemon Grass 6g £2.20p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52562487682"" owner=""38298328@N08"" secret=""43c782dc69"" server=""65535"" farm=""66"" title=""Lemon Myrtle 20g £3.70p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52562487512"" owner=""38298328@N08"" secret=""e19f2c4e56"" server=""65535"" farm=""66"" title=""Lemon Pyramid Salt Flakes 70g £4's, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563480358"" owner=""38298328@N08"" secret=""9c8bab8508"" server=""65535"" farm=""66"" title=""Lemongrass Powder 30g £2.20p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52562486987"" owner=""38298328@N08"" secret=""32daf5ed88"" server=""65535"" farm=""66"" title=""Mace Blade 18g £2.40p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563479678"" owner=""38298328@N08"" secret=""898af0c99d"" server=""65535"" farm=""66"" title=""Malaysian Satay 40g £2.30p and Szechuan Stir Fry 50g £2.40p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH (2)"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563225759"" owner=""38298328@N08"" secret=""0ae3df2aa2"" server=""65535"" farm=""66"" title=""Malaysian Satay 40g £2.30p and Szechuan Stir Fry 50g £2.40p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52562941276"" owner=""38298328@N08"" secret=""b86cceee63"" server=""65535"" farm=""66"" title=""Malaysian Satay 40g £2.30p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563479093"" owner=""38298328@N08"" secret=""32cc21bfa4"" server=""65535"" farm=""66"" title=""Mauritius Masala 40g £2.50p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563405500"" owner=""38298328@N08"" secret=""5270559c9c"" server=""65535"" farm=""66"" title=""Mexican Chipotle Salt 100g £3.10's, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563405295"" owner=""38298328@N08"" secret=""898b66abf5"" server=""65535"" farm=""66"" title=""Mild Madras 40g £2.30p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563224759"" owner=""38298328@N08"" secret=""4042e66580"" server=""65535"" farm=""66"" title=""Moroccan Lamb or Beef 40g £2.30p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52562940206"" owner=""38298328@N08"" secret=""0fb74a361e"" server=""65535"" farm=""66"" title=""Moruga Scorpion Fine Powder (Second HOTTEST Chilli) 10g £5.50p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563478123"" owner=""38298328@N08"" secret=""6c5dfd0870"" server=""65535"" farm=""66"" title=""Naga Curry HOT HOT HOT 40g £2.50p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH (2)"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52562939826"" owner=""38298328@N08"" secret=""8379021385"" server=""65535"" farm=""66"" title=""Naga Curry HOT HOT HOT 40g £2.50p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563404230"" owner=""38298328@N08"" secret=""ef89446808"" server=""65535"" farm=""66"" title=""Naka Flakes 20g £3.50p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563477513"" owner=""38298328@N08"" secret=""b792b4e0b0"" server=""65535"" farm=""66"" title=""Naka Jolokia 25g £3.50p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563223674"" owner=""38298328@N08"" secret=""2cfdb7cd80"" server=""65535"" farm=""66"" title=""Nasi Goreng 50g £2.40p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563476983"" owner=""38298328@N08"" secret=""18f4ebccc4"" server=""65535"" farm=""66"" title=""Nigella Seeds or Black Onion Seeds 50g £2's, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH (2)"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563222959"" owner=""38298328@N08"" secret=""39ecf67517"" server=""65535"" farm=""66"" title=""Nigella Seeds or Black Onion Seeds 50g £2's, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52562938156"" owner=""38298328@N08"" secret=""9db5d8e82d"" server=""65535"" farm=""66"" title=""Panag Curry 36g £2.50p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563401955"" owner=""38298328@N08"" secret=""66e3f3d249"" server=""65535"" farm=""66"" title=""Red Onion Flakes 30g £2's, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52562482242"" owner=""38298328@N08"" secret=""895f023d53"" server=""65535"" farm=""66"" title=""Piri Piri Chillies or Birdseye Chillies (Heat Scale Eight out of Ten) 30g £2.20p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563475218"" owner=""38298328@N08"" secret=""51a0729073"" server=""65535"" farm=""66"" title=""Ras El Hanout 30g £2.30p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52562481967"" owner=""38298328@N08"" secret=""18e07686ca"" server=""65535"" farm=""66"" title=""Red Miso 40g £2.30p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563474853"" owner=""38298328@N08"" secret=""cebcff49c9"" server=""65535"" farm=""66"" title=""Red Wine Infused Sea Salt 80g £4.20p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH (2)"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52562936506"" owner=""38298328@N08"" secret=""c8a511207a"" server=""65535"" farm=""66"" title=""Red Wine Infused Sea Salt 80g £4.20p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563474423"" owner=""38298328@N08"" secret=""0816f154f1"" server=""65535"" farm=""66"" title=""Rose Harissa 40g £2.40p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563400590"" owner=""38298328@N08"" secret=""8de22cd9d4"" server=""65535"" farm=""66"" title=""Rose Petals 8g £2's, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563400405"" owner=""38298328@N08"" secret=""c2e998bd60"" server=""65535"" farm=""66"" title=""Rosemary Pyramid Salt Flakes 70g £4's, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52562935621"" owner=""38298328@N08"" secret=""af9c62872a"" server=""65535"" farm=""66"" title=""Salade De La Mer Three Algae Seaweed 10g £2.10p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563473793"" owner=""38298328@N08"" secret=""f4c1e4202c"" server=""65535"" farm=""66"" title=""Salt and Pepper Scoops £4.50p each, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH (1)"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563399920"" owner=""38298328@N08"" secret=""71886aa4de"" server=""65535"" farm=""66"" title=""Salt and Pepper Scoops £4.50p each, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH (2)"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563472823"" owner=""38298328@N08"" secret=""31813b759a"" server=""65535"" farm=""66"" title=""Sicilian Orange Salt 100g £3.60p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52562934131"" owner=""38298328@N08"" secret=""63233d6be8"" server=""65535"" farm=""66"" title=""Smoked Chilli Flakes 30g £2.30p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563472128"" owner=""38298328@N08"" secret=""1c0409e6d1"" server=""65535"" farm=""66"" title=""Smoked Sugar 100g £4.50p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52562933616"" owner=""38298328@N08"" secret=""7282382851"" server=""65535"" farm=""66"" title=""Sour Cherries 60g £3's, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52563471843"" owner=""38298328@N08"" secret=""25bb588ee0"" server=""65535"" farm=""66"" title=""Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
		<photo id=""52562933351"" owner=""38298328@N08"" secret=""e25a8eb073"" server=""65535"" farm=""66"" title=""Spicy Jalfrezi 40g £2.40p, Spice Mountain, Borough Market, Borough High Street, Borough of Southwark, London, SE1 9AH"" ispublic=""1"" isfriend=""0"" isfamily=""0""/>
	</photos>
</rsp>";
        
        [SetUp]
        public void SetupTest()
        {
            processor = new FlickerResponseProcessor();
        }

        [Test]
        public void TestXMLSerialization()
        {
            bool processSuccessful = processor.SerializeResponse(sampleXml);
            Assert.IsTrue(processSuccessful);
			Assert.AreEqual(processor.GetResponsePages(), 1389);
			var array = processor.GetImagesUrl();
			Assert.AreEqual("https://live.staticflickr.com/65535/52563028656_b4989029eb.jpg", array[0]);
        }

		[Test]
		public void TestXMLDeserializationFailureOnEmptyString()
		{
            bool processSuccessful = processor.SerializeResponse("");
            Assert.IsFalse(processSuccessful);
        }

        [Test]
        public void TestXMLDeserializationFailureOnBadExample()
        {
            bool processSuccessful = processor.SerializeResponse(sampleXml + "badString");
            Assert.IsFalse(processSuccessful);
        }
    }
}
