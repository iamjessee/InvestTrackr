type Props = {
  config: any;
  data: any;
};

const Table = ({ config, data }: Props) => {
  const renderedRows = data.map((rowData: any, index: number) => {
    // Use index as fallback if no unique identifier is available
    return (
      <tr key={index}>
        {config.map((val: any, colIndex: number) => {
          return (
            <td key={`${index}-${colIndex}`} className="p-3">
              {val.render(rowData)}
            </td>
          );
        })}
      </tr>
    );
  });

  const renderedHeaders = config.map((config: any, index: number) => {
    return (
      <th
        scope="col"
        className="p-4 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
        key={index}
      >
        {config.label}
      </th>
    );
  });

  return (
    <div className="bg-white shadow rounded-lg p-4 sm:p-6 xl:p-8 ">
      <table className="min-w-full divide-y divide-gray-200 m-5">
        <thead className="bg-gray-50">
          <tr>{renderedHeaders}</tr>
        </thead>
        <tbody>{renderedRows}</tbody>
      </table>
    </div>
  );
};

export default Table;
