   M S B A M L   `   `   `  ���� P  LMultiscaleModelingApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:MultiscaleModelingAppMultiscaleModelingApp  &     MultiscaleModelingApp.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5local#clr-namespace:MultiscaleModelingApp       � Title$  
MainWindow��$��742.781��$��1108.021��.��� $��
0,0,-8,-21q� � ColumnDefinitions �� $
��177* ��� $
��931* �.+��� $	��X,Y��$
��Left=�$��
609,10,0,0q�$	��Top=�$��36��$��1���� -    ��Name$ XTextBox��$
��Left=�$��23��$��
652,14,0,0q� �� TextWrapping$
 Wrap=�$�� ��$	��Top=�$��86��$��1���� -   $ YTextBox��$
��Left=�$��23��$��
745,14,0,0q�$
 Wrap=�$�� ��$	��Top=�$��86��$��1���� -    ��Name$ 	MainPane1��$	��650��$��	10,30,0,0q�$	��Top=�$	��650��$
��Left=� �� CanVerticallyScroll . $��2��.+��� -    ��Name$ 	MainImage��$	��640��$	��640��$��10,0,0,0q� �� BitmapScalingMode$ NearestNeighbor=� �� EdgeMode$ Aliased=����� 	 �� 	UriSource$	  ?��� $��Label���� -   
 ��Name$
 DrawBtn��$
��Draw��$
��Left=�$��
839,16,0,0q�$	��Top=�$��68��$��1���� -   $
 SeedBtn��$��Nucleate��$
��Left=�$��
839,41,0,0q�$	��Top=�$��68��$��1���� -   $
 StartBtn��$��START��$
��Left=�$��
839,66,0,0q�$	��Top=�$��68��$��1���� -   $ SeedCountTxtBox��$
��Left=�$��19��$��
745,41,0,0q�$
 Wrap=�$�� ��$	��Top=�$��86��$��1��]� $
��Left=�$��23����.  $	��Top=�$	��660��$��2��.+�o� $	��Top=�.@�m� $��	Structure��.@�m� $��Import��.@�m� -	   $
��File��m� -
   $��Bitmap��m� $��Export��.@�m� -   $
��File��m� -   $��Bitmap���� $��Amount of inclusions��$��1��$
��Left=�$��
609,87,0,0q�$	��Top=��� -   $ AmountOfInclusionsTxtBox��$��1��$
��Left=�$��23��$��
745,91,0,0q�$
 Wrap=�$	��Top=�$	��162���� $��Size of inclusions��$��1��$
��Left=�$��609,118,0,0q�$	��Top=��� $��Type of inclusion��$��1��$
��Left=�$��609,149,0,0q�$	��Top=��� -   $ SizeOfInclusionsTxtBox��$��1��$
��Left=�$��23��$��745,121,0,0q�$
 Wrap=�$	��Top=�$	��162���� -    ��Name$ TypeOfInclusionComboBox��$��1��$
��Left=�$��745,153,0,0q�$	��Top=�$	��162�� �� SelectedIndex$ 0��.@��� $��circular���� $��diagonal���� -   $��Add inclusions��$��1��$
��Left=�$��745,192,0,0q�$	��Top=�$	��162���� -   $ TypeOfStructureCmbBox��$��1��$
��Left=�$��745,266,0,0q�$	��Top=�$	��162��.@��� $��Substructure���� $��
Dual Phase���� -   $
 	RegrowBtn��$��Regrow��$��1��$
��Left=�$��609,268,0,0q�$	��Top=�$	��122���� -   $ ProbabilityPercTxtBox��$��1��$
��Left=�$��23��$��857,228,0,0q�$
 Wrap=�$	��Top=�$��50���� $��Probability %��$��1��$
��Left=�$��765,225,0,0q�$	��Top=�$��87���� $��
Boundaries��$��1��$
��Left=�$��526,304,0,0q�$	��Top=��� -   $ TypeOfBoundaryCmbBox��$��1��$
��Left=�$��609,307,0,0q�$	��Top=�$	��120��.@��� $��
All grains���� $��Selected grains���� -   $
 DrawBoundariesBtn��$��Draw boundaries��$��1��$
��Left=�$��685,344,0,0q�$	��Top=�$	��135���� -   $
 ClearWithoutBoundariesBtn��$��Clear��$��1��$
��Left=�$��832,344,0,0q�$	��Top=�$��75���� $��Width of boundary��$��1��$
��Left=�$��745,304,0,0q�$	��Top=��� -   $ WidthOfBoundaryTxtBox��$��1��$
��Left=�$��23��$��860,308,0,0q�$
 Wrap=�$	��Top=�$��47���� -   $ NumberOfStatesTextBox��$��1��$
��Left=�$��23��$��633,419,0,0q�$
 Wrap=�$	��Top=�$��55���� -    ��Name$ NumberOfStatesLabel��$��Number of states��$��1��$
��Left=�$��526,416,0,0q�$	��Top=��� -   $
 NucleateMCBtn��$��
NucleateMC��$��1��$
��Left=�$��707,419,0,0q�$	��Top=�$��87��$��23���� -   $
 
StartMCBtn��$��StartMC��$��1��$
��Left=�$��814,419,0,0q�$	��Top=�$��73��$��23���� -   $ NumberOfIterationsTextBox��$��1��$
��Left=�$��23��$��633,450,0,0q�$
 Wrap=�$	��Top=�$��55��$��10���� -   $ NumberOfIterationsLabel��$��Number of iter��$��1��$
��Left=�$��526,447,0,0q�$	��Top=��� $ ��MC static recrystalization��$��1��$
��Left=�$��526,478,0,0q�$	��Top=�$	��381�����	#FFDE1212���	#FFEEEEEE�� $��Monte Carlo��$��1��$
��Left=�$��526,385,0,0q�$	��Top=�$	��381�����	#FFDE1212���	#FFEEEEEE�� $��Energy Distribution��$��1��$
��Left=�$��526,509,0,0q�$	��Top=��� -   $ EnergyDistribuitonCmbBox��$��1��$
��Left=�$��652,513,0,0q�$	��Top=�$	��120��$ 0��.@��� $��
Homogenous���� $��Heterogenous���� $��Energy inside��$��1��$
��Left=�$��526,540,0,0q�$	��Top=��� -   $ EnergyInsideTxtBox��$��1��$
��Left=�$��23��$��612,543,0,0q�$
 Wrap=�$	��Top=�$��69���� $��Energy egde��$��1��$
��Left=�$��697,541,0,0q�$	��Top=��� -    $ EnergyOnEgdesTxtBox��$��1��$
��Left=�$��23��$��783,544,0,0q�$
 Wrap=�$	��Top=�$��69���� -!   $
 ShowEnergyBtn��$��Show Energy��$��1��$
��Left=�$��526,664,0,0q�$	��Top=�$	��124���� $��Number of nucleons��$��1��$
��Left=�$��526,571,0,0q�$	��Top=��� -"   $ NUmberOfNucleonsTxtBox��$��1��$
��Left=�$��23��$��653,575,0,0q�$
 Wrap=�$	��Top=�$��55���� $��Location��$��1��$
��Left=�$��713,571,0,0q�$	��Top=��� -#   $ LocationCmbBox��$��1��$
��Left=�$��783,575,0,0q�$	��Top=�$	��120��$ 0��.@��� $��GB���� $��Anywhere���� $��Nucleation type��$��1��$
��Left=�$��526,602,0,0q�$	��Top=��� -$   $ NucleationTypeCmbBox��$��1��$
��Left=�$��625,606,0,0q�$	��Top=�$��84��.@��� $��Constant���� $��
Increasing���� $��	Beginning���� $��Number of iterations��$��1��$
��Left=�$��526,633,0,0q�$	��Top=��� -%   $# NUmberOfIterationsSRXMCTxtBox��$��1��$
��Left=�$��23��$��653,636,0,0q�$
 Wrap=�$	��Top=�$��55���� -&   $
 DIstributeEnergyBtn��$��Distribute Energy��$��1��$
��Left=�$��783,513,0,0q�$	��Top=�$	��124���� -'   $
 StartSRXMCBtn��$��Start SRXMC��$��1��$
��Left=�$��783,665,0,0q�$	��Top=�$	��124��